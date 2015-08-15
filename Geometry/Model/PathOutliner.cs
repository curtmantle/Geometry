using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Model
{
    public class PathOutliner : IPathOutliner
    {
        private Path path;
        private Point origin;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathOutliner"/> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="origin"></param>
        public PathOutliner(Path path, Point origin)
        {
            this.path = path;
            this.origin = origin;
        }

        public IPathOutline Generate()
        {
            var leftMost = path.StartPoint;//this.path.StartPoint.X <= this.path.EndPoint.X ? this.path.StartPoint : this.path.EndPoint;
            var rightMost = path.EndPoint;//this.path.StartPoint == leftMost ? this.path.EndPoint : this.path.StartPoint;

            //is the origin above

            var angleForLeft = GeometryHelper.GetAngleFromPoint(leftMost, this.path.Origin);
            var angleForRight = GeometryHelper.GetAngleFromPoint(rightMost, this.path.Origin);
            var pathOutline = new PathOutline()
            {
                BottomLeft = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius - (path.Width / 2), angleForLeft),
                TopLeft = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius + (path.Width / 2), angleForLeft),
                TopRight = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius + (path.Width / 2), angleForRight),
                BottomRight = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius - (path.Width / 2), angleForRight),
                Radius = path.Radius,
                Width = path.Width,
                Origin = path.Origin,
                LineColor = Colors.Black,
                LineWidth = 1,
                Direction = path.PathType
            };

            return pathOutline;
        }
    }
}
