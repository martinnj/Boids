using System;
using System.Collections.Generic;
using Datatypes.Geometry;
using Datatypes.Math;

namespace Datatypes.Boids
{
    /* Implements a simple "world" for boids to live in. Also includes the ability to
     * contain obstacles that boids cannot pass through.
     * The world will also be responsible for pdating the boids and calculate their movement.
     */
    class World
    {
        #region "Properties"
        public List<Boid> Boids { get; set; }
        public List<IGeometry> Obstacles { get; set; }
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
