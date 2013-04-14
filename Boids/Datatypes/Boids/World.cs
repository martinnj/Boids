using System;
using System.Collections.Generic;
using Datatypes.Geometry;
using Datatypes.Math;

namespace Datatypes.Boids
{
    /// <summary>
    /// Simple implementation of a "world".
    /// Will hold all the boids and obstacles and be responsible for performing the simulation.
    /// </summary>
    public class World
    {
        #region "Properties"
        /// <summary>
        /// List of Datatypes.Boids.Boid that represent all the living boids in the world.
        /// </summary>
        public List<Boid> Boids { get; set; }

        /// <summary>
        /// List of Datatypes.Geometry.IGeometry the represent all obstacles in the world.
        /// </summary>
        public List<IGeometry> Obstacles { get; set; }
        
        /// <summary>
        /// Hidden back-store for the size of the world.
        /// </summary>
        private Vector _size;
        /// <summary>
        /// Vector representing the multidimensional size of the world.
        /// </summary>
        /// <remarks>All boids with positions outside the world will be removed.</remarks>
        public Vector Size
        {
            get { return _size; }
            set {
                _size = value;
                foreach (var b in Boids)
                {
                    for (var i = 0; i < b.Position.Dimensions; i++ )
                    {
                        if (b.Position[i] >= 0 && b.Position[i] <= _size[i]) continue;
                        Boids.Remove(b);
                        break;
                    } 
                }
            }

        }

        #endregion

        #region "Constructors"
        /// <summary>
        /// Default constructor, creates a world with no boids and no obstacles and a size of 100x100x100.
        /// </summary>
        public World()
        {
            Boids = new List<Boid>();
            Obstacles = new List<IGeometry>();
            var s = new decimal[3];
            s[0] = 100;
            s[1] = 100;
            s[2] = 100;
            Size = new Vector(s);
        }

        #endregion

        #region "Boid movement"
        // Note: Hunting instinct is not currently implemented in the model.
        //       Neither is obstacles.

        // Controls a boids wish to stay close to other boids.
        private Vector Cohesion(Boid boid)
        {
            throw new NotImplementedException();
        }

        // Controls a boids wish to not collide with other boids
        // and avoid predators.
        private Vector Seperation(Boid boid)
        {
            throw new NotImplementedException();
        }

        // Controls a boids wish to fly in the same direction as other boids.
        private Vector Alignment(Boid boid)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
