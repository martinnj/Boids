using System;
using Datatypes.Math;

namespace Datatypes.Boids
{
    /// <summary>
    /// Simple class representing a boid.
    /// Class will only contain stats and not have any advanced methods.
    /// </summary>
    class Boid
    {
        #region "Properties"
        /// <summary>
        /// Datatypes.Math.Vector with the position of the boid in 3-dimensional space.
        /// </summary>
        public Vector Position;

        /// <summary>
        /// Datatypes.Math.Vector with the velocity of the boid in 3-dimensional space.
        /// </summary>
        public Vector Velocity;

        /// <summary>
        /// Integer value representing the boids placement in the foodchain. 0 means everyone can/will hunt it.
        /// </summary>
        public int    PredationLevel;
        //TODO: Implement hunger in later model.

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Default constructor, places the boid in 3-dimensional origo with no velocity and PredationLevel 0.
        /// </summary>
        public Boid()
        {
            Position = new Vector(3);
            Velocity = new Vector(3);
            PredationLevel = 0;
        }

        /// <summary>
        /// Simple boid constructor that allows to determine the boids initial position.
        /// Boid have no velocity and PredationLevel 0.
        /// </summary>
        /// <remarks>Velocity will  inherit dimension from position, allowing for N-dimension simulation.</remarks>
        /// <param name="position">The starting position for the boid.</param>
        public Boid(Vector position)
        {
            Position = position;
            Velocity = new Vector(position.Dimensions);
            PredationLevel = 0;
        }

        /// <summary>
        /// Simple boid constructor that allows to determine the boids initial position and PredationLevel.
        /// Boid have no velocity.
        /// </summary>
        /// <remarks>Velocity will  inherit dimension from position, allowing for N-dimension simulation.</remarks>
        /// <remarks>predationLevel must be at least zero.</remarks>
        /// <param name="position">The starting position for the boid.</param>
        /// <param name="predationLevel">Int giving the predation level of the boid.</param>
        /// <exception cref="System.ArgumentException">Thrown if the predation level is below zero.</exception>
        public Boid(Vector position, int predationLevel)
        {
            if(predationLevel < 0) throw new ArgumentException("predationLevel cannot be less than zero.");
            Position = position;
            Velocity = new Vector(position.Dimensions);
            PredationLevel = predationLevel; 
        }

        #endregion
    }
}
