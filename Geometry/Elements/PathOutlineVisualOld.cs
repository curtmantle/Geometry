using Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Elements
{

    /// <summary>
    /// Renders the outline of a path
    /// </summary>
    public class PathOutlineVisualOld : DrawingVisual
    {
        public PathOutlineVisualOld()
        {
            
        }

        public PathOutlineVisualOld(IPathOld path, Color color)
        {
            var brush = new SolidColorBrush(color);
            var pen = new Pen(brush, 1);

            var geometry = new StreamGeometry();
            using (var gc = geometry.Open())
            {

                gc.BeginFigure(path.StartBottomEdge, false, false);
                gc.LineTo(path.StartTopEdge, true, true);
                gc.ArcTo(path.EndTopEdge, new Size(path.Radius+(path.Width/2), path.Radius+(path.Width/2)), 1, false, SweepDirection.Clockwise, true, true);
                gc.LineTo(path.EndBottomEdge, true, true);
                gc.ArcTo(path.StartBottomEdge, new Size(path.Radius-(path.Width/2), path.Radius-(path.Width/2)), 1, false, SweepDirection.Counterclockwise, true, true);

            }

            using (var context = RenderOpen())
            {
                context.DrawGeometry(brush, pen, geometry);

                var points = CalculateSteps(path);

                foreach (var point in points)
                {
                    DrawPoint(point, Brushes.Blue, context);
                }
            }
        }

        private void DrawPoint(Point pt, Brush brush, DrawingContext context)
        {
            var pen = new Pen(brush, 1);
            context.DrawEllipse(brush, pen, pt, 2, 2);
        }

        private Point[] CalculateSteps(IPathOld path)
        {
            var stepDistance = 10;
            var pt1 = path.StartPoint;
            var pt2 = path.EndPoint;
            var origin = path.Origin;
            var radius = path.Radius;

            //calculate the angle of the start and end point from the origin
            var startPointAngle = GeometryHelper.GetAngleFromPoint(pt1, origin);
            var endPointAngle = GeometryHelper.GetAngleFromPoint(pt2, origin);
            var angleOfPoints = CalculateAngleOfPoints(startPointAngle, endPointAngle);

            //determine the length of the curve
            var circumference = Math.PI * radius;
            var divisor = 360 / angleOfPoints;

            //calculate the degrees of each step
            var numberOfStepsInCircle = Math.Floor(circumference / stepDistance);
            var stepAngle = 360 / numberOfStepsInCircle;
            int numberOfStepsInCurve = (int)Math.Floor(numberOfStepsInCircle / divisor);
            var currentAngle = startPointAngle;
            var result = new Point[numberOfStepsInCurve*2];
            for (int i = 0; i < numberOfStepsInCurve; i++)
            {
                currentAngle += stepAngle;
                result[i*2] = GeometryHelper.GetPointAtAngle(origin, radius - (path.Width/4), currentAngle);
                result[i * 2 + 1] = GeometryHelper.GetPointAtAngle(origin, radius + (path.Width/4), currentAngle);
            }
            return result;
        }

        private double CalculateAngleOfPoints(double angle1, double angle2)
        {
            if (angle2 < angle1)
            {
                return (360 - angle1) + angle2;
            }

            return angle2 - angle1;
        }
    }
}
