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
    public class PathOutlineVisual : DrawingVisual
    {
        public PathOutlineVisual()
        {

        }

        public PathOutlineVisual(IPathOutline outline)
        {
            var brush = new SolidColorBrush(outline.LineColor);
            var pen = new Pen(brush, 1);

            var geometry = new StreamGeometry();
            using (var gc = geometry.Open())
            {

                gc.BeginFigure(outline.BottomLeft, false, false);
                gc.LineTo(outline.TopLeft, true, true);
                gc.ArcTo(outline.TopRight, 
                    new Size(outline.Radius + (outline.Width / 2), outline.Radius + (outline.Width / 2)), 
                    1, 
                    false, 
                    outline.Direction == PathType.Convex ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, 
                    true, 
                    true);

                gc.LineTo(outline.BottomRight, true, true);
                gc.ArcTo(outline.BottomLeft, 
                    new Size(outline.Radius - (outline.Width / 2), outline.Radius - (outline.Width / 2)), 
                    1, 
                    false, 
                    outline.Direction == PathType.Convex ? SweepDirection.Counterclockwise : SweepDirection.Clockwise, 
                    true, 
                    true);

            }

            using (var context = RenderOpen())
            {
                context.DrawGeometry(brush, pen, geometry);
            }
        }

    }
}
