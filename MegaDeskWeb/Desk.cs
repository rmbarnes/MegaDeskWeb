using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaDeskWeb
{
    public struct Desk
    {
        public int Width { get; set; }
        public int Depth { get; set; }
        public int NumDrawers { get; set; }
        public Surface SurfaceMaterial { get; set; }

        public enum Surface
        {
            Pine, //0
            Laminate, //1
            Oak, //2
            Veneer, //3
            Rosewood //4
        }
    }
}