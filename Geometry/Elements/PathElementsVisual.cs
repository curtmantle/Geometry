using Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Elements
{
    public class PathElementsVisual : DrawingVisual
    {
        public PathElementsVisual()
        {

        }

        private void CreateArrows(Path path, StreamGeometryContext gc)
        {
            double start;

            if (path.PathType == PathType.Convex)
            {
                start = GeometryHelper.GetAngleFromPoint(path.StartPoint, path.Origin);
            }
            else
            {
                start = GeometryHelper.GetAngleFromPoint(path.EndPoint, path.Origin);
            }

            for(int i= 0; i < 10; i++)
            {
                start += 8;
                var org = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius, start);
                var pt1 = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius + 10, start);
                var pt2 = GeometryHelper.GetPointAtAngle(path.Origin, path.Radius - 10, start);
                var pt3 = GeometryHelper.GetPointAtAngle(org, 20, start + 90);

                gc.BeginFigure(pt1, true, true);
                gc.LineTo(pt2, true, true);
                gc.LineTo(pt3, true, true);
                gc.LineTo(pt1, true, true);

                gc.BeginFigure(path.Origin, false, false);
                gc.LineTo(pt1, true, true);

            }
        }

        public PathElementsVisual(Path path)
        {
            var geometry = new StreamGeometry();
            using(var gc = geometry.Open())
            {

                CreateArrows(path, gc);

            }

            using (var context = RenderOpen())
            {
                var linePen = new Pen(Brushes.Red, 1);
                context.DrawLine(linePen, path.StartPoint, path.EndPoint);

                var firstCirclePen = new Pen(Brushes.Green, 1);
                context.DrawEllipse(Brushes.Transparent, firstCirclePen, path.CurveCircles[0], path.Radius, path.Radius);

                var secondCirclePen = new Pen(Brushes.Orange, 1);
                context.DrawEllipse(Brushes.Transparent, secondCirclePen, path.CurveCircles[1], path.Radius, path.Radius);

                context.DrawGeometry(Brushes.Black, new Pen(Brushes.Black, 1), geometry);
            }
        }
    }
}
