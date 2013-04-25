using System;
using System.Collections.Generic;
using System.Linq;
using Datatypes.Geometry;
using Datatypes.Math;

namespace Datatypes.Boids
{
    /// <summary>
    /// Simple implementation of a "world", will be acting as the simulation engine.
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

        /// <summary>
        /// The maximum speed a boid can travel.
        /// TODO: Make this independent for each boid.
        /// </summary>
        public decimal MaxSpeed;
        private decimal _maxForce;
        private decimal _minSeperation;

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
            MaxSpeed = 3;
            _maxForce = Convert.ToDecimal(0.05);
            _minSeperation = 6;
        }

        #endregion

        #region "Boid movement"
        // Note: Hunting instinct is not currently implemented in the model.
        //       Neither is obstacles.

        /// <summary>
        /// Updates the world, performs one "tick" meaning all objects gets
        /// updated with one fictive time unit.
        /// </summary>
        public void Tick()
        {
            foreach (var b in Boids)
            {
                UpdateBoid(b);
            }
        }

        /// <summary>
        /// Updates the given boid with a single tick.
        /// </summary>
        /// <param name="b">The boid to be updated.</param>
        private void UpdateBoid(Boid b)
        {
            /* Construct a list of boids with same predationlevel
             * Only use that list for updating positions
             * TODO: Implement hunting and fleeing.
             */
            var allies = Boids.Where(boid => boid.PredationLevel == b.PredationLevel && boid != b).ToList();
            var c = Cohesion(b, allies);
            var s = Seperation(b, allies);
            var a = Alignment(b, allies);

            //TODO: Add weights for movement (cohesion is most important etc)
            var acceleration = c + s + a;
            b.Velocity += acceleration;
            b.Velocity = Limit(b.Velocity, MaxSpeed);
            b.Position += b.Velocity;
        }

        // Controls a boids wish to stay close to other boids.
        private Vector Cohesion(Boid boid, List<Boid> allies)
        {
            if(allies.Count < 1) { return new Vector(boid.Position.Dimensions); }
            var c = new Vector(boid.Position.Dimensions);
            c = allies.Aggregate(c, (current, ally) => current + ally.Position);
            c /= allies.Count();
            return SteerTo(boid, c);
        }

        // Calculates the actual "motion" from the desired destination.
        private Vector SteerTo(Boid boid, Vector desiredDestination)
        {
            var desired = Vector.Subtract(boid.Position, desiredDestination);
            var d = desired.Magnitude();
            if (d > 0)
            {
                desired.Normalize();
                if (d < 100)
                {
                    desired.Scale(MaxSpeed*Convert.ToDecimal(d/100));
                }
                else
                {
                    desired.Scale(MaxSpeed);
                }
                var steer = desired;
                steer.Subtract(boid.Velocity);
                steer = Limit(steer, _maxForce);
                return steer;
            }
            return new Vector(boid.Position.Dimensions);
        }

        // Controls a boids wish to not collide with other boids
        // and avoid predators.
        private Vector Seperation(Boid boid, IEnumerable<Boid> allies)
        {
            var mean = new Vector(boid.Position.Dimensions);
            var count = 0;
            foreach (var ally in allies)
            {
                var d = Vector.Subtract(ally.Position, boid.Position).Magnitude();
                if (d > 0 && Convert.ToDecimal(d) < _minSeperation)
                {
                    var t = Vector.Subtract(ally.Position, boid.Position);
                    t.Normalize();
                    t.Div(Convert.ToDecimal(d));
                    mean.Add(t);
                    count++;
                }
            }
            if (count > 0)
            {
                mean.Div(count);
            }
            return mean;
        }

        // Controls a boids wish to fly in the same direction as other boids.
        private Vector Alignment(Boid boid, List<Boid> allies)
        {
            var mean = new Vector(boid.Position.Dimensions);
            var count = 0;
            foreach (var ally in allies)
            {
                mean += ally.Velocity;
                count++;
            }
            if (count > 0)
            {
                mean.Div(count);
            }
            mean = Limit(mean, _maxForce);
            return mean;
        }

        // Limit are both set on positive and negative side of origo.
        private static Vector Limit(Vector a, decimal limit)
        {
            if (Convert.ToDecimal(a.Magnitude()) > limit)
            {
                a.Normalize();
                return a*Convert.ToDecimal(limit);
            }
            return a;
        }

        #endregion
    }
}
