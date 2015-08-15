using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Model
{

    public class PathOutline : IPathOutline
    {
        public Point BottomLeft { get; set; }
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomRight { get; set; }
        public int Radius { get; set; }
        public Point Origin { get; set; }
        public int Width { get; set; }
        public Color LineColor { get; set; }
        public int LineWidth { get; set; }
        public PathType Direction { get; set; }
    }
}
