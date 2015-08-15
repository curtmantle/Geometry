using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Elements
{
    public class CurvedSegment : DrawingVisual
    {        
        
        
        private delegate void RenderDelegate(DrawingContext context, int i, Point origin, double currentAngle, int radius);

        public CurvedSegment()
        {


            // Draw the geometry
            using (var context = RenderOpen())
            {
                var radius = 50;
                var requiredDistance = 20;
                var origin = new Point(200, 200);
                var renderers = new List<RenderDelegate>() { PlotPoints, PlotLinesAroundCircle };

                RenderPointsAroundOrigin(context, origin, radius, requiredDistance, renderers);

                PlotLinesAtAngle(context, origin, radius, 180);
                
            }
        }

        private static void RenderPointsAroundOrigin(DrawingContext context, Point origin, int radius, int requiredDistance, IList<RenderDelegate> renderers)
        {
            var circumference = Math.PI * (radius * 2);
            var numberOfPlots = circumference / requiredDistance;
            var angleOfEach = 360 / numberOfPlots;
            var iterations = Math.Floor(numberOfPlots);
            var pen = new Pen(Brushes.Black, 1);



            //draw center point
            context.DrawEllipse(Brushes.Black, pen, origin, 1, 1);

            for (var i = 0; i < iterations; i++)
            {
                var currentAngle = i * angleOfEach;

                foreach(var renderer in renderers)
                {
                    renderer(context, i, origin, currentAngle, radius);              
                }

            }
        }


        private void  PlotPoints(DrawingContext context, int i, Point origin, double currentAngle, int radius) 
        {
            var pen = new Pen(Brushes.Red, 4);

            var point = GetPointAtAngle(origin, radius, currentAngle);
            var point2 = GetPointAtAngle(origin, radius - 10, currentAngle);
            var point3 = GetPointAtAngle(origin, radius + 10, currentAngle);

            //centre point
            context.DrawEllipse(Brushes.Black, pen, point, 1, 1);
            //inner point
            context.DrawEllipse(Brushes.Black, pen, point2, 1, 1);
            //outer point
            context.DrawEllipse(Brushes.Black, pen, point3, 1, 1);
        }

        private void PlotLinesAroundCircle(DrawingContext context, int i, Point origin, double currentAngle, int radius) 
        {
            var pen = new Pen(Brushes.Green, 1);


            var point1 = GetPointAtAngle(origin, radius+10, currentAngle);

            context.DrawLine(pen, origin, point1);

        }


        private void PlotLinesAtAngle(DrawingContext context, Point origin, double angle, int radius)
        {
            var pen = new Pen(Brushes.Orange, 2);

            var point1 = GetPointAtAngle(origin, radius + 10, angle);

            context.DrawLine(pen, origin, point1);
        }
        /// <summary>
        /// Calculates a point the around a circle at the specified angle
        /// </summary>
        /// <param name="origin">Origin of circle</param>
        /// <param name="radius">Radius of circle</param>
        /// <param name="angle">Angle to draw</param>
        /// <returns>Point</returns>
        private Point GetPointAtAngle(Point origin, int radius, double angle)
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
    }
}
