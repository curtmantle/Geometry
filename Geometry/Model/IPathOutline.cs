using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Model
{
    public interface IPathOutline
    {
        /// <summary>
        /// Bottom Left of the outline
        /// </summary>
        Point BottomLeft { get; }

        /// <summary>
        /// Top Left of the outline
        /// </summary>
        Point TopLeft { get; }

        /// <summary>
        /// Top Right of the outline
        /// </summary>
        Point TopRight { get; }

        /// <summary>
        /// Bottom Right of the outline
        /// </summary>
        Point BottomRight { get; }

        /// <summary>
        /// The Origin of the circle that forms the curve
        /// </summary>
        Point Origin { get; }

        /// <summary>
        /// The radius of the circle that forms the curve
        /// </summary>
        int Radius { get; }

        /// <summary>
        /// Width of the path
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Color of the line
        /// </summary>
        Color LineColor { get; }

        /// <summary>
        /// The widht of the line
        /// </summary>
        int LineWidth { get; }

        PathType Direction { get; }
    }
}
