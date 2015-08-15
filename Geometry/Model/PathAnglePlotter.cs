using System;
using System.Collections.Generic;
using System.Linq;

namespace Geometry.Model
{
    /// <summary>
    /// Calculates the positions of the nodes on the path
    /// </summary>
    public class PathNodePlotter
    {
        private Path path;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathNodePlotter"/> class.
        /// </summary>
        /// <param name="path"></param>
        public PathNodePlotter(Path path)
        {
            this.path = path;
        }


    }

    /// <summary>
    /// Calculates the angles of the steps along the centre of the path
    /// </summary>
    /// <remarks>
    /// The step angles represent the angle from the origin of each step 
    /// along the curve of the path. With the angles it's easy to calculate
    /// any point on that angle at any distance from the origin.
    /// </remarks>
    public class PathAngleCalculator
    {
        private Path path;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathAnglePlotter"/> class.
        /// </summary>
        /// <param name="path"></param>
        public PathAngleCalculator(Path path)
        {
            this.path = path;
        }

        /// <summary>
        /// Calculates the angles
        /// </summary>
        /// <remarks>
        /// The calculations are separated out for ease of understanding and debugging
        /// </remarks>
        /// <returns></returns>
        public double[] Calculate()
        {
            var startPointAngle = GeometryHelper.GetAngleFromPoint(path.StartPoint, path.Origin);
            var endPointAngle = GeometryHelper.GetAngleFromPoint(path.EndPoint, path.Origin);
            var angleOfPoints = CalculateAngleOfPoints(startPointAngle, endPointAngle);

            var circumference = Math.PI * (path.Radius*2);
            var lengthOfCurve = circumference / (360 / angleOfPoints);

            var numberOfStepsInCurve = (int)(lengthOfCurve / path.StepDistance);
            var angleBetweenSteps = angleOfPoints / numberOfStepsInCurve;

            var angles = new double[numberOfStepsInCurve];
            var currentAngle = startPointAngle + (angleBetweenSteps/2);

            for (int i = 0; i < numberOfStepsInCurve; i++)
            {
                angles[i] = currentAngle;                
                currentAngle += angleBetweenSteps;
            }
            return angles;
        }

        public double CalculateAnglePerUnit()
        {
            var circumference = Math.PI * (path.Radius * 2);
            var anglePerUnit = 360 / circumference;
            return anglePerUnit;

        }

        /// <summary>
        /// Calculates the angle between the two points
        /// </summary>
        /// <param name="angle1"></param>
        /// <param name="angle2"></param>
        /// <returns></returns>
        private double CalculateAngleOfPoints(double angle1, double angle2)
        {
            if (angle2 < angle1)
            {
                if (path.PathType == PathType.Concave)
                    return angle1 - angle2;
                else
                    return (360 - angle1) + angle2;
            }

            return angle2 - angle1;
        }
    }
}
