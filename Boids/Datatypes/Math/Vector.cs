using System.Collections.Generic;
using Datatypes.Exceptions;

namespace Datatypes.Math
{
    /*
     * Class used to represent an N-dimension vector
     */
    public class Vector
    {
        public int Dimensions { get; private set; }
        private decimal[] _values;

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
        protected decimal this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        #region "Math"
        public static Vector Add(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) throw new InvalidVectorDimensions("a and b have different dimensions.");
            var c = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                c[i] = a[i] + b[i];
            }
            return c;
        }

        public static Vector Subtract(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions) throw new InvalidVectorDimensions("a and b have different dimensions.");
            var c = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                c[i] = a[i] - b[i];
            }
            return c;
        }

        public static Vector Scale(Vector a, int scalar)
        {
            var res = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                res[i] = a[i] * scalar;
            }
            return res;
        }

        #endregion

        #region "Old code"
        //// coordinate variables.
        //public decimal X;
        //public decimal Y;
        //public decimal Z;

        //// Constructor
        //public Vector(decimal x, decimal y, decimal z)
        //{
        //    this.X = x;
        //    this.y = y;
        //    this.z = z;
        //}

        //#region "functions for math stuffs"
        //// adds two vectors
        //public static vector add(vector A, vector B)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res.x = A.x + B.x;
        //    res.y = A.y + B.y;
        //    res.z = A.z + B.z;
        //    return res;
        //}

        //// subtracts two vectors
        //public static vector sub(vector A, vector B)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res.x = A.x - B.x;
        //    res.y = A.y - B.y;
        //    res.z = A.z - B.z;
        //    return res;
        //}

        //// function for multiplying a scalar unto the vector
        //public static vector scale(vector A, int scalar)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res.x = A.x * scalar;
        //    res.y = A.y * scalar;
        //    res.z = A.z * scalar;
        //    return res;
        //}

        //// function for dividing a vector with a scalar
        //// scalar prevented from being 0.
        //public static vector div(vector A, int scalar)
        //{
        //    vector res = new vector(0, 0, 0);
        //    scalar = scalar==0 ? 1:scalar;
        //    res.x = A.x / scalar;
        //    res.y = A.y / scalar;
        //    res.z = A.z / scalar;
        //    return res;
        //}

        //// function to multiply 2 vectors (cross product)
        //public static vector multiply(vector A, vector B)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res.x = A.y * B.z - A.z * B.y;
        //    res.y = A.z * B.x - A.x * B.z;
        //    res.z = A.x * B.y - A.y * B.x;
        //    return res;
        //}

        //// function to compute "dot product" for 2 vectors
        //public static decimal dot(vector A, vector B)
        //{
        //    return A.x * B.x + A.y*B.y + A.z*B.z;
        //}

        //// function that returns the length of the vector
        //// TODO:FIXME: EEEWWW A lot of casting going on here!!
        //public static decimal length(vector A)
        //{
        //    return (decimal) Math.Sqrt((double) vector.dot(A,A));
        //}

        //// function to invert a vector (i.e. change sign)
        //public static vector invert(vector A)
        //{
        //    return new vector(-A.x, -A.y, -A.z);
        //}

        //#endregion

        //#region "operator overrides"
        //// override +operator to use vectors.
        //public static vector operator +(vector A, vector B)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res = add(A, B);
        //    return res;
        //}

        //// override -operator to use vectors.
        //public static vector operator -(vector A, vector B)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res = sub(A, B);
        //    return res;
        //}

        //// override -operator to use as inverter
        //public static vector operator -(vector A)
        //{
        //    return invert(A);
        //}

        //// override *operator to use vectors.
        //public static vector operator *(vector A, vector B)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res = multiply(A, B);
        //    return res;
        //}

        //// override *operator to use vector and scalar.
        //public static vector operator *(vector A, int scalar)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res = scale(A, scalar);
        //    return res;
        //}

        //// override /operator
        //public static vector operator /(vector A, int scalar)
        //{
        //    vector res = new vector(0, 0, 0);
        //    res = div(A, scalar);
        //    return res;
        //}

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
