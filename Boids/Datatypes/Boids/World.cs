using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
