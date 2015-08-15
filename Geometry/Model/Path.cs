using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Model
{
    public class Path
    {
        private IPathOutline outline;
        private IPathOutliner outliner;

        private Point startPoint;
        private Point endPoint;        
        private int width;
        private int radius;
        private int stepDistance;

        private PathType direction;

        private Point[] originCircles;

        public Path(Point startPoint, Point endPoint, int radius, int width, PathType direction)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.radius = radius;
            this.width = width;
            this.direction = direction;
            this.StepDistance = 40;
            CalculateOrigin();

            this.outliner = new PathOutliner(this, this.Origin);
        }

        public IPathOutline Outline
        {
            get
            {
                if (this.outline == null && this.outliner != null)
                {
                    this.outline = this.outliner.Generate();
                }

                return outline;
            }
        }

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
            }
        }

        public Point Origin
        {
            get
            {
                return this.PathType == PathType.Concave ? this.originCircles[1] : this.originCircles[0];
            }
        }

        public Point[] CurveCircles
        {
            get { return this.originCircles; }
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
            }
        }

        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        public int StepDistance
        {
            get
            {
                return stepDistance;
            }
            set
            {
                stepDistance = value;
            }
        }

        public PathType PathType
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        private void CalculateOrigin()
        {
            if (StartPoint == EndPoint)
            {
                originCircles = GetEmptyOriginCircles();
            }
            else
            {
                originCircles = GeometryHelper.FindCircles(StartPoint, EndPoint, Radius);

                if (originCircles == null)
                {
                    originCircles = GetEmptyOriginCircles();
                }
            }
        }

        private Point[] GetEmptyOriginCircles()
        {
            return new Point[] { new Point(StartPoint.X, StartPoint.Y),
                                 new Point(StartPoint.X, StartPoint.Y) };
            
        }
    }
}
