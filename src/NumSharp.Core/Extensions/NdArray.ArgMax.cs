﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumSharp.Core.Extensions
{
    public static partial class NDArrayExtensions
    {
        public static int ArgMax(this NDArrayGeneric<double> np )
        {
            var max = np.Data.Max();

            return np.Data.ToList().IndexOf(max);
        }
        public static int ArgMax(this NDArrayGeneric<int> np )
        {
            var max = np.Data.Max();

            return np.Data.ToList().IndexOf(max);
        }
    }
}
