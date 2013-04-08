using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datatypes.Math;

namespace DataTypes.UnitTests
{
    [TestClass]
    public class MathUnitTests
    {
        [TestMethod]
        public void VectorTests()
        {
            // Arrange test data.
            var a = new Vector(2);
            var b = new Vector(2);
            var d = new decimal[2];
            d[0] = 6;
            d[1] = 123123123123;
            a[0] = 4;
            a[1] = 2;
            var c = new Vector(d);


            // Perform operations.
            b = Vector.Add(a, b);
            c = Vector.Subtract(c, b);
            a = Vector.Scale(a, 2);

            // Assert results.
            Assert.AreEqual(a[0], 8);
            Assert.AreEqual(a[1], 4);
            Assert.AreEqual(b[0], 4);
            Assert.AreEqual(b[1], 2);
            Assert.AreEqual(c[0], 2);
            Assert.AreEqual(c[1], 123123123121);
        }
    }
}
