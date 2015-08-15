using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Geometry.Model
{
    public static class GeometryHelper
    {
        /// <summary>
        /// Calculates a point the around a circle at the specified angle
        /// </summary>
        /// <param name="origin">Origin of circle</param>
        /// <param name="radius">Radius of circle</param>
        /// <param name="angle">Angle to draw</param>
        /// <returns>Point</returns>
        public static Point GetPointAtAngle(Point origin, int radius, double angle)
        {
            var t = DegreeToRadian(angle);
            var x = radius * Math.Cos(t) + origin.X;
            var y = radius * Math.Sin(t) + origin.Y;

            return new Point(x, y);
        }

        /// <summary>
        /// Converts an angle to radians
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public  static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        /// <remarks>
        /// Distance between two points is Square root of (x1-x2)^2 + (y1-y2)^2
        /// </remarks>
        /// <param name="pt1">First Point</param>
        /// <param name="pt2">Second Point</param>
        /// <returns>Distance between points</returns>
        public static double DistanceBetweenPoints(Point pt1, Point pt2)
        {
            return Math.Sqrt(Math.Pow(pt1.X - pt2.X, 2) + (Math.Pow(pt1.Y-pt2.Y, 2)));
        }

        /// <summary>
        /// Gets the centre of two circles that intersect two points
        /// </summary>
        /// <remarks>
        /// Given two points and a radius, the centre of the two circles is:
        /// 
        /// Square Root of radius^2 - (distanceBetweenPoints/2)^2
        /// </remarks>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <param name="radius">radius of the circles</param>
        /// <returns></returns>
        public static Point[] FindCircles(Point p1, Point p2, double radius)
        {
            var separation = DistanceBetweenPoints(p1, p2);

            if (separation == 0 || separation == (2*radius) || separation > (2*radius))
            {
                return null;
            }

            var mirrorDistance = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(separation / 2, 2));
        
            var x1 = (p1.X+p2.X)/2 + mirrorDistance*(p1.Y-p2.Y)/separation;
            var y1 = (p1.Y+p2.Y)/2 + mirrorDistance*(p2.X-p1.X)/separation;
            var centre1 = new Point(x1, y1);

            var x2 = (p1.X+p2.X)/2 - mirrorDistance*(p1.Y-p2.Y)/separation; 
            var y2 = (p1.Y+p2.Y)/2 - mirrorDistance*(p2.X-p1.X)/separation;
            var centre2 = new Point(x2, y2);

            return new Point[] { centre1, centre2 };
        }

        public static double GetAngleFromPoint(Point point, Point centerPoint)
        {
            double dy = (point.Y - centerPoint.Y);
            double dx = (point.X - centerPoint.X);

            double theta = Math.Atan2(dy, dx);

            double angle = ((theta * 180) / Math.PI) % 360;
            if (angle < 0)
                angle = 360 + angle;
            return angle;
        }

        /// <summary>
        /// Calculates a point the around a circle at the specified angle
        /// </summary>
        /// <param name="origin">Origin of circle</param>
        /// <param name="radius">Radius of circle</param>
        /// <param name="angle">Angle to draw</param>
        /// <returns>Point</returns>
        public static Point GetPointAtAngle(Point origin, double radius, double angle)
        {
            var t = DegreeToRadian(angle);
            var x = radius * Math.Cos(t) + origin.X;
            var y = radius * Math.Sin(t) + origin.Y;

            return new Point(x, y);
        }

    }
}
