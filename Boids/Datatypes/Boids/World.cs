using System.Collections.Generic;
using Datatypes.Geometry;

namespace Datatypes.Boids
{
    /* Implements a simple "world" for boids to live in. Also includes the ability to
     * contain obstacles that boids cannot pass through.
     */
    class World
    {
        #region "Properties"
        public List<Boid> Boids { get; set; }
        public List<IGeometry> Obstacles { get; set; }
        #endregion
    }
}
