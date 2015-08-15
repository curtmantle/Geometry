using Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Geometry.Elements
{
    public class PathStepVisual : DrawingVisual
    {
        public PathStepVisual()
        {

        }

        public PathStepVisual(Path path)
        {
            var brush = Brushes.Blue;
            var pen = new Pen(brush, 1);

            var stepPlotter = new PathAngleCalculator(path);
            var angles = stepPlotter.Calculate();
            var anglePerUnit = stepPlotter.CalculateAnglePerUnit();
            var upperCurve = path.Radius + (path.Width/4);
            var lowerCurve = path.Radius - (path.Width/4);
            const int unitSize = 15;
            const int unitSizeDivisor = 3;
            var geometry = new StreamGeometry();
            using(var gc = geometry.Open())
            { 
                foreach (var angle in angles)
                {
                    var angleMovement = angle - ((anglePerUnit * unitSize)/2);
                    var centrePoint = GeometryHelper.GetPointAtAngle(path.Origin, upperCurve, angle);
                    var lower = GeometryHelper.GetPointAtAngle(path.Origin, upperCurve - unitSize/unitSizeDivisor, angleMovement);
                    var upper = GeometryHelper.GetPointAtAngle(path.Origin, upperCurve + unitSize/unitSizeDivisor, angleMovement);
                    var centre = GeometryHelper.GetPointAtAngle(path.Origin, upperCurve, angleMovement);
                    var point = GeometryHelper.GetPointAtAngle(centre, unitSize, angle + 90);

                    gc.BeginFigure(lower, true, true);
                    gc.LineTo(upper, true, true);
                    gc.LineTo(point, true, true);
                    gc.LineTo(lower, true, true);

                    angleMovement = angle + ((anglePerUnit * unitSize) / 2);
                    centrePoint = GeometryHelper.GetPointAtAngle(path.Origin, lowerCurve, angle);
                    lower = GeometryHelper.GetPointAtAngle(path.Origin, lowerCurve - unitSize / unitSizeDivisor, angleMovement);
                    upper = GeometryHelper.GetPointAtAngle(path.Origin, lowerCurve + unitSize / unitSizeDivisor, angleMovement);
                    centre = GeometryHelper.GetPointAtAngle(path.Origin, lowerCurve, angleMovement);
                    point = GeometryHelper.GetPointAtAngle(centre, unitSize, angle - 90);

                    gc.BeginFigure(lower, true, true);
                    gc.LineTo(upper, true, true);
                    gc.LineTo(point, true, true);
                    gc.LineTo(lower, true, true);
                }            
            }

            using (var context = RenderOpen())
            {
                context.DrawGeometry(brush, pen, geometry);
            }
        }

        
    }
}
