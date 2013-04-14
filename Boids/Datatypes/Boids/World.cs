using System.Collections.Generic;
using Datatypes.Geometry;

namespace Datatypes.Boids
{
    class World
    {
        #region "Variables"

        public List<Boid> Boids { get; set; }
        public List<IGeometry> Obstacles { get; set; }

        #endregion
    }
}
