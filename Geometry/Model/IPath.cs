using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Geometry.Model
{

    public interface IPathOld
    {
        int Width { get; set; }
        Point EndBottomEdge { get; }
        Point EndTopEdge { get; }
        Point StartPoint { get; set; }
        Point EndPoint { get; set; }
        int Diameter { get; set; }

        Point Origin { get; }
        double Radius { get; }

        
        Point StartTopEdge { get; }
        Point StartBottomEdge { get; }

        double StartAngleFromOrigin { get; }         
        double EndAngleFromOrigin { get; }
    }
}
