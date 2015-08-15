using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using Geometry.Model;
namespace Geometry.Elements
{

    public class CirclesForPointsVisual : DrawingVisual 
    {
        public CirclesForPointsVisual()
        {

        }

        public CirclesForPointsVisual(Point pt1, Point pt2, int radius)
        {
            //first we want to plot the two points 

            using (var context = RenderOpen())
            {
                DrawPoint(pt1, Brushes.Red, context);
                DrawPoint(pt2, Brushes.Red, context);
                DrawLineBetweenPoints(pt1, pt2, context);

                var circles = GeometryHelper.FindCircles(pt1, pt2, radius);

                foreach(var circle in circles)
                {
                    DrawPoint(circle, Brushes.Green, context);
                    DrawCircle(circle, radius,  Brushes.Green, context);
                    DrawPoint(circle, Brushes.Orange, context);
                    DrawCircle(circle, radius,  Brushes.Orange, context);
                }

                var clockWiseGeometry = CreateArc(pt1, pt2, radius, SweepDirection.Clockwise);

                context.DrawGeometry(Brushes.Violet, new Pen(Brushes.Violet, 5), clockWiseGeometry);

                var antiClockwiseGeometry = CreateArc(pt1, pt2, radius, SweepDirection.Counterclockwise);

                context.DrawGeometry(Brushes.Lime, new Pen(Brushes.Lime, 5), antiClockwiseGeometry);


                var angleOfPoint = GeometryHelper.GetAngleFromPoint(pt1, circles[0]);
                if (angleOfPoint < 0)
                    angleOfPoint = 360 + angleOfPoint;
                var angledPoint = GetPointAtAngle(circles[0], radius, angleOfPoint);

                DrawLineBetweenPoints(circles[0], angledPoint, context);

                angleOfPoint = GeometryHelper.GetAngleFromPoint(pt2, circles[0]);

                angledPoint = GetPointAtAngle(circles[0], radius, angleOfPoint);

                DrawLineBetweenPoints(circles[0], angledPoint, context);

                var points = CalculateSteps(pt1, pt2, circles[0], radius);

                foreach(var point in points)
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

        private void DrawLineBetweenPoints(Point pt1, Point pt2, DrawingContext context)
        {
            var pen = new Pen(Brushes.Green, 1);

            context.DrawLine(pen, pt1, pt2);
        }

        private void DrawCircle(Point origin, int radius, Brush brush, DrawingContext context)
        {
            context.DrawEllipse(Brushes.Transparent, new Pen(brush, 1), origin, radius, radius);
        }

        private StreamGeometry CreateArc(Point pt1, Point pt2, int radius, SweepDirection direction)
        {
            var geometry = new StreamGeometry();
            using (var gc = geometry.Open())
            {
                gc.BeginFigure(pt1, false, false);

                gc.ArcTo(pt2, new Size(radius, radius), 1, false, direction, true, true);

            }

            return geometry;
        }

        /// <summary>
        /// Calculates a point the around a circle at the specified angle
        /// </summary>
        /// <param name="origin">Origin of circle</param>
        /// <param name="radius">Radius of circle</param>
        /// <param name="angle">Angle to draw</param>
        /// <returns>Point</returns>
        private Point GetPointAtAngle(Point origin, double radius, double angle)
        {
            var t = DegreeToRadian(angle);
            var x = radius * Math.Cos(t) + origin.X;
            var y = radius * Math.Sin(t) + origin.Y;

            return new Point(x, y);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private Point[] CalculateSteps(Point pt1, Point pt2, Point origin, double radius)
        {
            var stepDistance = 10;
            

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
            var result = new Point[numberOfStepsInCurve];
            for (int i = 0; i < numberOfStepsInCurve; i++)
            {
                currentAngle += stepAngle;
                result[i] = GetPointAtAngle(origin, radius, currentAngle);
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
