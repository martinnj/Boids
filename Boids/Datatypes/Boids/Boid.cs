using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datatypes.Math;

namespace Datatypes.Boids
{
    /* Implements a simple boid
     * with basic properties.
     */
    class Boid
    {
        #region "Properties"

        public Vector Position;
        public Vector Velocity;
        public int    PredationLevel;
        //TODO: Implement in later model.
        // public int    HungerLevel;

        #endregion
    }
}
