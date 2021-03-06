﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumSharp.Core
{
    public partial class NDArray
    {
        /// <summary>
        /// Return the maximum of an array or minimum along an axis
        /// </summary>
        /// <param name="np"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public NDArray amax(int? axis = null)
        {
            var res = new NDArray(dtype);

            double[] npArr = this.Storage.GetData<double>();

            if (axis == null)
            {
                double min = npArr[0];
                for (int i = 0; i < npArr.Length; i++)
                    min = Math.Max(min, npArr[i]);

                res.Storage  = NDStorage.CreateByArray(new double[1] {min});                
            }
            else
            {
                if (axis < 0 || axis >= this.ndim)
                    throw new Exception("Invalid input: axis");
                int[] resShapes = new int[this.shape.Shapes.Count - 1];
                int index = 0; //index for result shape set
                //axis departs the shape into three parts: prev, cur and post. They are all product of shapes
                int prev = 1;
                int cur = 1;
                int post = 1;
                int size = 1; //total number of the elements for result
                //Calculate new Shape
                for (int i = 0; i < this.shape.Shapes.Count; i++)
                {
                    if (i == axis)
                        cur = this.shape.Shapes[i];
                    else
                    {
                        resShapes[index++] = this.shape.Shapes[i];
                        size *= this.shape.Shapes[i];
                        if (i < axis)
                            prev *= this.shape.Shapes[i];
                        else
                            post *= this.shape.Shapes[i];
                    }
                }
                res.Storage.Shape = new Shape(resShapes);
                //Fill in data
                index = 0; //index for result data set
                int sameSetOffset = this.shape.DimOffset[axis.Value];
                int increments = cur * post;
                double[] resData = new double[size];  //res.Data = new double[size];
                int start = 0;
                double min = 0;
                for (int i = 0; i < this.size; i += increments)
                {
                    for (int j = i; j < i + post; j++)
                    {
                        start = j;
                        min = npArr[start];
                        for (int k = 0; k < cur; k++)
                        {
                            min = Math.Max(min, npArr[start]);
                            start += sameSetOffset;
                        }
                        resData[index++] = min;
                    }
                }
                res.Storage = NDStorage.CreateByArray(resData);
                res.Storage.Shape = new Shape(resShapes);
            }
            return res;
        }
    }
}
