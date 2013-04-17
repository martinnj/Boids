using System;
using System.Text;
using Datatypes.Exceptions;

namespace Datatypes.Math
{
    /// <summary>
    /// Class providing access to functions for manipulating vectors.
    /// Instanciable to represent a N-dimension vector by itself.
    /// </summary>
    public class Vector
    {

        #region "Properties"
        /// <summary>
        /// Integer giving the number of dimensions the vector is constructed in.
        /// </summary>
        public int Dimensions { get; private set; }
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        private static decimal[] _values;
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        #endregion

        #region "Constructors"
        /// <summary>
        /// Initialises an instance the vector class with all fields set to 0.
        /// </summary>
        /// <param name="dimension">number of dimensions (fields) the vector should have.</param>
        public Vector(int dimension)
        {
            Dimensions = dimension;
            _values = new decimal[dimension];
            for (var i = 0; i < _values.Length; i++)
            {
                _values[i] = 0;
            }
        }

        /// <summary>
        /// Initialises an instance of the vector class with values from a decimal array.
        /// The vectors dimensions will be the length of the value array.
        /// </summary>
        /// <param name="values">decimal array with values to put in the vector.</param>
        public Vector(decimal[] values)
        {
            _values = values;
            Dimensions = values.Length;
        }
        #endregion

        /// <summary>
        /// Access to the fields in the vector.
        /// </summary>
        /// <param name="i">Index/dimension to retrieve decimal from.</param>
        /// <returns>The decimal stored in the i'th field.</returns>
        public decimal this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        #region "Math"
        /// <summary>
        /// Adds 2 vectors together and return the resulting vector.
        /// </summary>
        /// <param name="a">first N-dimension vector for addition.</param>
        /// <param name="b">second N-dimension vector for addition.</param>
        /// <returns>an N-dimension vector with the result.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimensions of the a/b-vectors are not the same.</exception>
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

        /// <summary>
        /// Perform a vector addition on this N-dimension vector.
        /// </summary>
        /// <param name="a">N-dimension vector to use for addition.</param>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimension of the a-vectors isnot the same as the instances.</exception>
        public void Add(Vector a)
        {
            if (a.Dimensions != Dimensions) throw new InvalidVectorDimensions("a dimensions(" + a.Dimensions + ") does not match own dimensions(" + Dimensions + ").");
            for (var i = 0; i < a.Dimensions; i++)
            {
                _values[i] += _values[i];
            }
        }

        /// <summary>
        /// Subtracts 2 vectors and return the resulting vector.
        /// </summary>
        /// <param name="a">first N-dimension vector for subtraction.</param>
        /// <param name="b">second N-dimension vector for subtraction.</param>
        /// <returns>an N-dimension vector with the result.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimensions of the a/b-vectors are not the same.</exception>
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

        /// <summary>
        /// Perform a vector subtraction on this N-dimension vector.
        /// </summary>
        /// <param name="a">N-dimension vector to use for subtraction.</param>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimension of the a-vectors isnot the same as the instances.</exception>
        public void Subtract(Vector a)
        {
            if (a.Dimensions != Dimensions) throw new InvalidVectorDimensions("a dimensions(" + a.Dimensions + ") does not match own dimensions(" + Dimensions + ").");
            for (var i = 0; i < a.Dimensions; i++)
            {
                _values[i] -= a[i];
            }
        }

        /// <summary>
        /// Scale a vector with a decimal scalar.
        /// </summary>
        /// <param name="a">N-dimension vector to scale values in.</param>
        /// <param name="scalar">scalar to use.</param>
        /// <returns>N-dimension vector result.</returns>
        public static Vector Scale(Vector a, decimal scalar)
        {
            var res = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                res[i] = a[i] * scalar;
            }
            return res;
        }

        /// <summary>
        /// Scale the vector with a decimal scalar.
        /// </summary>
        /// <param name="s">decimal scalar to apply.</param>
        public void Scale(decimal s)
        {
            for (var i = 0; i < Dimensions; i++)
            {
                _values[i] *= s;
            }
        }

        /// <summary>
        /// Negative scaling of a vector using division.
        /// </summary>
        /// <param name="a">N-dimension vector to scale values in.</param>
        /// <param name="scalar">decimal scalar to use.</param>
        /// <returns>N-dimension vector result.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the scalar is zero.</exception>
        public static Vector Div(Vector a, decimal scalar)
        {
            if (scalar == 0) throw new ArgumentException("Scalar cannot be zero.");
            var b = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                b[i] = a[i] / scalar;
            }
            return b;
        }

        /// <summary>
        ///  Negative scaling of the vector using division.
        /// </summary>
        /// <param name="scalar">decimal scalar to use.</param>
        /// <exception cref="System.ArgumentException">Thrown if the scalar is zero.</exception>
        public void Div(decimal scalar)
        {
            if (scalar == 0) throw new ArgumentException("Scalar cannot be zero.");
            for (var i = 0; i < Dimensions; i++)
            {
                _values[i] /= scalar;
            }
        }

        /// <summary>
        /// Calculates the cross product of two N-dimension vectors.
        /// </summary>
        /// <param name="a">first N-dimension vector for cross product.</param>
        /// <param name="b">second N-dimension vector for cross product.</param>
        /// <returns>The resulting N-dimension vector.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimensions of the a/b-vectors are not the same.</exception>
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

        /// <summary>
        /// Calculates the cross product of a N-dimension vector and this vector.
        /// </summary>
        /// <param name="a">N-dimension vector for cross product.</param>
        /// <returns>The resulting N-dimension vector.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimension of the a-vectors is not the same as the instances.</exception>
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

        /// <summary>
        /// Calculates the dot product of two N-dimension vectors.
        /// </summary>
        /// <param name="a">first N-dimension vector for dot product.</param>
        /// <param name="b">second N-dimension vector for dot product.</param>
        /// <returns>The resulting N-dimension vector.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimensions of the a/b-vectors are not the same.</exception>
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

        /// <summary>
        /// Calculates the dot product of a N-dimension vector and this vector.
        /// </summary>
        /// <param name="a">N-dimension vector for dot product.</param>
        /// <returns>The resulting N-dimension vector.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimension of the a-vectors is not the same as the instances.</exception>
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

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="a">N-dimension vector to calculate length for.</param>
        /// <returns>a double containing the magnitude of the vector.</returns>
        public static double Magnitude(Vector a)
        {
            //TODO: find a way to avoid the conversion from decimal to double.
            return System.Math.Sqrt( Convert.ToDouble(a.Dot(a)) );
        }

        /// <summary>
        /// Returns the magnitude of the vector.
        /// </summary>
        /// <returns>a double containing the magnitude of the vector.</returns>
        public double Magnitude()
        {
            //TODO: find a way to avoid the conversion from decimal to double.
            return System.Math.Sqrt( Convert.ToDouble(Dot(this)) );
        }

        /// <summary>
        /// Normalize the N-dimensional vector A.
        /// </summary>
        /// <param name="a">N-dimensional vector to be normalized.</param>
        /// <returns>N-dimensional normalized vector.</returns>
        public static Vector Normalize(Vector a)
        {
            var r = a;
            r.Normalize();
            return r;
        }

        /// <summary>
        /// Normalize this vector.
        /// </summary>
        public void Normalize()
        {
            var m = this.Magnitude();
            for (var i = 0; i < _values.Length; i++)
            {
                _values[i] = _values[i]/Convert.ToDecimal(m);
            }
        }

        /// <summary>
        /// Inverts the values in the vectors field (sign change).
        /// </summary>
        /// <param name="a">a N-dimension vector to invert.</param>
        /// <returns>the N-dimension vector resulting from the inversion.</returns>
        public static Vector Inverse(Vector a)
        {
            var b = new Vector(a.Dimensions);
            for (var i = 0; i < a.Dimensions; i++)
            {
                b[i] = a[i] * -1;
            }
            return b;
        }

        /// <summary>
        /// Invert this vector (sign change).
        /// </summary>
        public void Inverse()
        {
            for (var i = 0; i < Dimensions; i++)
            {
                this[i] *= -1;
            }
        }
        #endregion

        #region "Equality functions"

        /// <summary>
        /// Checks if a object instance of the Vector class is equal this vector.
        /// Equal in this sense is they share the same dimensions and values.
        /// </summary>
        /// <param name="obj">Object Vector to compare to self.</param>
        /// <returns>true if they are equal, false if they're not.</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null) return false; // If object is null, return false.
            var a = obj as Vector;
            if ((Object) a == null) return false; // If object cannot be cast to Vector, return false.

            // Do actual vector matching.
            if (a.Dimensions != Dimensions) return false;
            var res = true;
            for (var i = 0; i < a.Dimensions; i++)
            {
                res &= (a[i] == _values[i]);
            }
            return res;
        }

        /// <summary>
        /// Checks if two objects instances of the Vector class are the equal vector.
        /// Equal in this sense is they share the same dimensions and values. 
        /// </summary>
        /// <param name="a">first vector to compare.</param>
        /// <param name="b">second vector to compare.</param>
        /// <returns>true if they are equal, false if they're not.</returns>
        public static bool Equals(Vector a, Vector b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks if a instance of the Vector class is equal this vector.
        /// Equal in this sense is they share the same dimensions and values.
        /// </summary>
        /// <param name="a">Vector to compare to self.</param>
        /// <returns>true if they are equal, false if they're not.</returns>>
        protected bool Equals(Vector a)
        {
            if (a == null) throw new ArgumentNullException("a");
            if (a.Dimensions != Dimensions) return false;
            var res = true;
            for (var i = 0; i < a.Dimensions; i++)
            {
                res &= (a[i] == _values[i]);
            }
            return res;
        }

        /// <summary>
        /// Generates a hashcode for this instance of the vector.
        /// The hashcode is the sum of the number of dimensions, and the vector dotted with itself.
        /// </summary>
        /// <returns>an integer hashcode.</returns>
        public override int GetHashCode()
        {
            //TODO: Fix this, it will break for long arrays with big numbers.
            return Convert.ToInt32(Dimensions + Dot(this,this));
        }
        #endregion

        #region "Operator overrides"
        /// <summary>
        /// Operator override to make "+" add 2 vectors together and return the resulting vector.
        /// </summary>
        /// <param name="a">first N-dimension vector for addition.</param>
        /// <param name="b">second N-dimension vector for addition.</param>
        /// <returns>an N-dimension vector with the result.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimensions of the a/b-vectors are not the same.</exception>
        public static Vector operator +(Vector a, Vector b)
        {
            return Add(a, b);
        }

        /// <summary>
        /// Operator override to make "-" subtract 2 vectors and return the resulting vector.
        /// </summary>
        /// <param name="a">first N-dimension vector for subtraction.</param>
        /// <param name="b">second N-dimension vector for subtraction.</param>
        /// <returns>an N-dimension vector with the result.</returns>
        /// <exception cref="Datatypes.Exceptions.InvalidVectorDimensions">Thrown when the dimensions of the a/b-vectors are not the same.</exception>
        public static Vector operator -(Vector a, Vector b)
        {
            return Subtract(a, b);
        }

        /// <summary>
        /// Operator override to make "-" invert the values in the vectors fields (sign change).
        /// </summary>
        /// <param name="a">a N-dimension vector to invert.</param>
        /// <returns>the N-dimension vector resulting from the inversion.</returns>
        public static Vector operator -(Vector a)
        {
            return Inverse(a);
        }

        /// <summary>
        /// Operator override to make "*" scale a vector with a decimal scalar.
        /// </summary>
        /// <param name="a">N-dimension vector to scale values in.</param>
        /// <param name="scalar">scalar to use.</param>
        /// <returns>N-dimension vector result.</returns>
        public static Vector operator *(Vector a, decimal scalar)
        {
            return Scale(a, scalar);
        }
        //TODO: Find sensible overrides for cross/dot.

        /// <summary>
        /// Operator override to make "/" negative scale a vector using division.
        /// </summary>
        /// <param name="a">N-dimension vector to scale values in.</param>
        /// <param name="scalar">decimal scalar to use.</param>
        /// <returns>N-dimension vector result.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the scalar is zero.</exception>
        public static Vector operator /(Vector a, decimal scalar)
        {
            return Div(a, scalar);
        }

        /// <summary>
        /// Operator override to make "==" able to compare to vector instances.
        /// Equal in this sense is they share the same dimensions and values.
        /// </summary>
        /// <param name="a">first vector to compare.</param>
        /// <param name="b">second vector to compare.</param>
        /// <returns>true if they are equal, false if not.</returns>
        public static bool operator ==(Vector a, Vector b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// Operator override to make "!=" able to compare to vector instances.
        /// Equal in this sense is they share the same dimensions and values.
        /// </summary>
        /// <param name="a">first vector to compare.</param>
        /// <param name="b">second vector to compare.</param>
        /// <returns>false if they are equal, true if not.</returns>
        public static bool operator !=(Vector a, Vector b)
        {
            return !Equals(a, b);
        }

        #endregion

        #region "Convinience functions"

        /// <summary>
        /// Gets the string representation of the vector.
        /// </summary>
        /// <returns>a string representation of the vector.</returns>
        public new string ToString()
        {
            var sb = new StringBuilder(); // Use stringbuffer to prevent concat-performance-drop when using massive vectors.
            sb.Append("[");
            for (var i = 0; i < Dimensions; i++)
            {
                sb.Append(_values[i]);
                sb.Append(" , ");
            }
            sb.Remove(sb.Length - 3, 3);
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Gets the string representation of a vector.
        /// </summary>
        /// <param name="a">the vector to get representation from.</param>
        /// <returns>a string representation of the vector a.</returns>
        public static string ToString(Vector a)
        {
            return a.ToString();
        }

        #endregion
    }
}
