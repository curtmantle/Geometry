using Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Geometry
{
    public class Context
    {
        private Path firstPath;
        private Path secondPath;

        public Context()
        {
            firstPath = new Path(new Point(50, 200), new Point(450, 200), 220, 100, PathType.Convex);
            //secondPath = new Path(new Point(450, 150), new Point(550, 600), 280, 100, PathDirection.Convex);

        }

        public Path FirstPath
        {
            get { return this.firstPath; }
        }

        public Path SecondPath
        {
            get
            {
                return secondPath;
            }
        }



    }
}
