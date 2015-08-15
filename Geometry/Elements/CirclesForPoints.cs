using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;

namespace Geometry.Elements
{
    public class CirclesForPoints : UIElement
    {
        private Point pt1 = new Point(100, 100);
        private Point pt2 = new Point(250, 200);

        private CirclesForPointsVisual _visual;

        public CirclesForPoints()
        {
            _visual = new CirclesForPointsVisual(pt1, pt2, 100);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visual;
        }

        public int Radius
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
                return (int)GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public static readonly DependencyProperty RadiusProperty = 
            DependencyProperty.Register("Radius", typeof(int), typeof(CirclesForPoints), 
            new FrameworkPropertyMetadata(20, new PropertyChangedCallback(OnRadiusChanged)));

        private static void OnRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as CirclesForPoints;
            element._visual = new CirclesForPointsVisual(element.pt1, element.pt2, element.Radius);
        }

        
        
    }
}
