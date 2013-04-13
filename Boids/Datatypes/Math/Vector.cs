using System;
using Datatypes.Exceptions;

namespace Datatypes.Math
{
    /*
     * Class used to represent an N-dimension vector
     */
    public class Vector
    {
        #region "Variables"
        public int Dimensions { get; private set; }
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        private static decimal[] _values;
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        #endregion

        #region "Constructors"
        // Initialises the N-dimension vector with all fields set to 0.
        public Vector(int dimension)
        {
            Dimensions = dimension;
            _values = new decimal[dimension];
            for (var i = 0; i < _values.Length; i++)
            {
                _values[i] = 0;
            }
        }

        // Initialise the vector with values from a decimal array.
        public Vector(decimal[] values)
        {
            _values = values;
            Dimensions = values.Length;
        }
        #endregion

        // Add the ability to index directly in the vectors values.
        public decimal this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        #region "Math"
        // Add two vectors together and return the resulting vector.
        public static Vector Add(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) throw new InvalidVectorDimensions("a and b have different dimensions. (" + a.Dimensions + " != " + b.Dimensions + ")");
            var c = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                c[i] = a[i] + b[i];
            }
            return c;
        }

        // Add a vector to"this" vector.
        public void Add(Vector a)
        {
            if (a.Dimensions != Dimensions) throw new InvalidVectorDimensions("a dimensions(" + a.Dimensions + ") does not match own dimensions(" + Dimensions + ").");
            for (var i = 0; i < a.Dimensions; i++)
            {
                _values[i] += _values[i];
            }
        }

        // Subtract two vectors and return the resulting vector.
        public static Vector Subtract(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) throw new InvalidVectorDimensions("a and b have different dimensions. (" + a.Dimensions + " != " + b.Dimensions + ")");
            var c = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                c[i] = a[i] - b[i];
            }
            return c;
        }

        // Subtract a vector from "this" vector.
        public void Subtract(Vector a)
        {
            if (a.Dimensions != Dimensions) throw new InvalidVectorDimensions("a dimensions(" + a.Dimensions + ") does not match own dimensions(" + Dimensions + ").");
            for (var i = 0; i < a.Dimensions; i++)
            {
                _values[i] -= a[i];
            }
        }

        // Scale a vector with as scalar.
        public static Vector Scale(Vector a, int scalar)
        {
            var res = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                res[i] = a[i] * scalar;
            }
            return res;
        }

        // Scale "this" vector with a scalar.
        public void Scale(int s)
        {
            for (var i = 0; i < Dimensions; i++)
            {
                _values[i] *= s;
            }
        }

        // Divide a vector with a scalar
        // Will throw exception if scalar is 0.
        public static Vector Div(Vector a, int s)
        {
            if (s == 0) throw new ArgumentException("Scalar cannot be zero.");
            var b = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                b[i] = a[i]/s;
            }
            return b;
        }

        // Divide "this" vector with a scalar
        // Will throw exception if scalar is 0.
        public void Div(int s)
        {
            if (s == 0) throw new ArgumentException("Scalar cannot be zero.");
            for (var i = 0; i < Dimensions; i++)
            {
                _values[i] /= s;
            }
        }

        // Calculate the cross-product of two vectors of same dimensions.
        public static Vector Cross(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) throw new InvalidVectorDimensions("a and b have different dimensions. (" + a.Dimensions + " != " + b.Dimensions + ")");
            var c = new Vector(a.Dimensions);
            var d = a.Dimensions;
            for (var i = 0; i < a.Dimensions; i++)
            {
                c[i] = a[(i + 1) % d] * b[(i + 2) % d] - a[(i + 2) % d] * b[(i + 1) % d];
            }
            return c;
        }

        // Cross a vector with "this" vector.
        public Vector Cross(Vector a)
        {
            if (a.Dimensions != Dimensions) throw new InvalidVectorDimensions("a dimensions(" + a.Dimensions + ") does not match own dimensions(" + Dimensions + ").");
            var c = new Vector(a.Dimensions);
            var me = new Vector(_values); // Create a copy of original values to prevent screwing up results when i wraps around.
            var d = a.Dimensions;
            for (var i = 0; i < a.Dimensions; i++)
            {
                _values[i] = me[(i + 1) % d] * a[(i + 2) % d] - me[(i + 2) % d] * a[(i + 1) % d];
            }
            return c;
        }

        // Calculate the dot product of 2 vectors of same dimensions.
        public static decimal Dot(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) throw new InvalidVectorDimensions("a and b have different dimensions. (" + a.Dimensions + " != " + b.Dimensions + ")");
            decimal sum = 0;
            for (var i = 0; i < a.Dimensions; i++)
            {
                sum += a[i] + b[i];
            }
            return sum;
        }

        // Calculate the dot product of this vector and another vector with same dimensions.
        public decimal Dot(Vector a)
        {
            if (a.Dimensions != Dimensions) throw new InvalidVectorDimensions("a dimensions(" + a.Dimensions + ") does not match own dimensions(" + Dimensions + ").");
            decimal sum = 0;
            for (var i = 0; i < a.Dimensions; i++)
            {
                sum += a[i] + _values[i];
            }
            return sum;
        }

        // Returns the length of a vector.
        public static double Length(Vector a)
        {
            //TODO: find a way to avoid the conversion from decimal to double.
            return System.Math.Sqrt( Convert.ToDouble(a.Dot(a)) );
        }

        // Return the length of this vector. Note this is not the Dimensions!
        public double Length()
        {
            //TODO: find a way to avoid the conversion from decimal to double.
            return System.Math.Sqrt( Convert.ToDouble(Dot(this)) );
        }

        // Invert a vector.
        public static Vector Inverse(Vector a)
        {
            var b = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                b[i] = a[i] * -1;
            }
            return b;
        }

        // Inverts "this" vector.
        public void Inverse()
        {
            for (var i = 0; i < Dimensions; i++)
            {
                this[i] *= -1;
            }
        }
        #endregion

        #region "Equality functions"
        // Checks if two vectors are equal to one another in terms of dimensions and values.
        public static bool Equals(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) return false;
            var res = true;
            for (var i = 0; i < a.Dimensions; i++)
            {
                res &= (a[i] == b[i]);
            }
            return res;
        }
        #endregion

        #region "Operator overrides"
        // Override plus operator for vector addition.
        public static Vector operator +(Vector a, Vector b)
        {
            return Add(a, b);
        }

        // Override minus operator for vector subtraction.
        public static Vector operator -(Vector a, Vector b)
        {
            return Subtract(a, b);
        }

        // Override minus operator for vector inversion.
        public static Vector operator -(Vector a)
        {
            return Inverse(a);
        }

        // Override asterisk operator for vector scaling.
        public static Vector operator *(Vector a, int s)
        {
            return Scale(a, s);
        }
        //TODO: Find sensible overrides for cross/dot.

        // Override slash operator for vector division/scaling.
        public static Vector operator /(Vector a, int s)
        {
            return Div(a, s);
        }

        // Override equality operators for vector comparison.
        public static bool operator ==(Vector a, Vector b)
        {
            throw new NotImplementedException();
        }
        public static bool operator !=(Vector a, Vector b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Old code"

        //// override ==operator to use vectors.
        //public static bool operator ==(vector A, vector B)
        //{
        //    bool res = Equals(A, B);
        //    return res;
        //}

        //// override !=operator to use vectors.
        //public static bool operator !=(vector A, vector B)
        //{
        //    bool res = !Equals(A, B);
        //    return res;
        //}
        //#endregion

        //#region "comparison functions"

        //// compare instantiated vector to some other one
        //public bool Equals(vector B)
        //{
        //    bool bx = this.X == B.x;
        //    bool by = this.y == B.y;
        //    bool bz = this.z == B.z;
        //    return bx && by && bz;
        //}
        //#endregion


        //public override string ToString()
        //{
        //    return "[" + X + "," + y + "," + z + "]";
        //}

        ///* Note:
        // * This function is a crude implement only used for
        // * controlling the velocity of my boids during simulation
        // * TODO: Find a way to limit speed via behavior this is BAD
        // */
        //public void limit(int i)
        //{
        //    // upper limits
        //    X = X < i ? X : i;
        //    y = y < i ? y : i;
        //    z = z < i ? z : i;
        //    // lower limits
        //    X = X > -i ? X : -i;
        //    y = y > -i ? y : -i;
        //    z = z > -i ? z : -i;

        //}
        #endregion
    }
}
