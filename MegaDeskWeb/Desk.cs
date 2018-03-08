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
            Pine = 1, //1
            Rosewood = 2, //2
            Veneer = 3, //3
            Laminate = 4, //4
            Oak = 5 //5             
        }
    }
}