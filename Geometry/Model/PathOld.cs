using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Geometry.Model
{
    public class PathOld : IPathOld
    {
        #region Private Fields


        private Point endPoint;
        private Point startPoint;
        private Point origin;
        private double startAngleFormOrigin;
        private double endAngleFromOrigin;
        private int diameter;
        private int width;

        public PathOld()
        {
            this.startPoint = new Point(0, 0);
            this.endPoint = new Point(0, 0);
            this.diameter = 0;
            this.width = 0;
            CalculateOrigin();
            CalculateAngle();
        }

        public PathOld(Point start, Point end, int diameter, int width)
        {

            this.startPoint = start;
            this.endPoint = end;
            this.diameter = diameter;
            this.width = width;
            CalculateOrigin();
            CalculateAngle();
        }

        #endregion

        public Point StartPoint
        {
            get
            {
                return startPoint;
            }
            set
            {

                if (startPoint == value)
                    return;

                startPoint = value;

                CalculateOrigin();
                CalculateAngle();
            }
        }

        public Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                if (endPoint == value)
                    return;
                endPoint = value;

                CalculateOrigin();
                CalculateAngle();
            }
        }


        public int Diameter
        {
            get
            {
                return diameter;
            }
            set
            {
                if (diameter == value)
                    return;
                diameter = value;

                CalculateOrigin();
                CalculateAngle();
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;

                CalculateOrigin();
                CalculateAngle();
            }
        }

        public double Radius
        {
            get { return diameter / 2; }
        }

        public Point Origin
        {
            get
            {
                return origin;
            }
        }

        public double StartAngleFromOrigin
        {
            get
            {
                return startAngleFormOrigin;
            }
        }

        public double EndAngleFromOrigin
        {
            get
            {
                return endAngleFromOrigin;
            }
        }

        public Point StartBottomEdge
        {
            get
            {
                return GeometryHelper.GetPointAtAngle(Origin, Radius - (Width / 2), StartAngleFromOrigin);
            }
        }

        public Point StartTopEdge
        {
            get
            {
                return GeometryHelper.GetPointAtAngle(Origin, Radius + (Width / 2), StartAngleFromOrigin);
            }
        }

        public Point EndBottomEdge
        {
            get
            {
                return GeometryHelper.GetPointAtAngle(Origin, Radius - (Width / 2), EndAngleFromOrigin);
            }
        }

        public Point EndTopEdge
        {
            get
            {
                return GeometryHelper.GetPointAtAngle(Origin, Radius + (Width / 2), EndAngleFromOrigin);
            }
        }

        private void CalculateOrigin()
        {
            if (StartPoint == EndPoint)
            {
                origin = new Point(StartPoint.X, StartPoint.Y);

            }
            else
            {
                var circles = GeometryHelper.FindCircles(StartPoint, EndPoint, Radius);

                if (circles != null)
                {
                    origin = circles[0];
                }
            }
        }

        private void CalculateAngle()
        {
            bool noLength = StartPoint == Origin;

            this.startAngleFormOrigin = !noLength ? GeometryHelper.GetAngleFromPoint(StartPoint, Origin) : 0;
            this.endAngleFromOrigin = !noLength ? GeometryHelper.GetAngleFromPoint(EndPoint, Origin) : 0;
        }


    }
}
