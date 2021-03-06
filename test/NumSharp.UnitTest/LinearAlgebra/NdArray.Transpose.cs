using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using NumSharp.Core.Extensions;
using System.Linq;
using NumSharp.Core;

namespace NumSharp.UnitTest.LinearAlgebra
{
    [TestClass]
    public class TransposeTest : TestBase
    {
        [TestMethod]
        public void TwoxThree()
        {
            NDArray np1 = np.arange(6).reshape(3,2);
            
            var np1Transposed = np1.transpose();

            Assert.AreEqual(np1Transposed[0,0], 0);
            Assert.AreEqual(np1Transposed[0,1], 2);
            Assert.AreEqual(np1Transposed[0,2], 4);
            Assert.AreEqual(np1Transposed[1,0], 1);
            Assert.AreEqual(np1Transposed[1,1], 3);
            Assert.AreEqual(np1Transposed[1,2], 5);

        }
    }
}
