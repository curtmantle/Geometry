using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Elements
{
    public class CurvedSegmentContainer : UIElement
    {
        private CurvedSegment _visual = new CurvedSegment();

        protected override Visual GetVisualChild(int index)
        {
            return _visual;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
    }
}
