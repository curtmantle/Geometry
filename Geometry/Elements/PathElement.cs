using Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Elements
{
    public class PathElement : FrameworkElement
    {
        private PathOutlineVisual _visual;
        
        private DrawingVisual[] visuals;

        public PathElement()
        {
            visuals = new DrawingVisual[2];
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get { return visuals.Count(); }
        }

        public Path Path
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
                return (Path)GetValue(PathProperty);
            }
            set
            {
                SetValue(PathProperty, value);
            }
        }

        public static readonly DependencyProperty PathProperty = 
            DependencyProperty.Register("Path", 
                typeof(Path), 
                typeof(PathElement), 
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(PathChanged)));

        private static void PathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dpo = d as PathElement;

            if (dpo != null)
            {
                var path = e.NewValue as Path;
                if (path != null)
                {
                    dpo.visuals[0] = new PathOutlineVisual(path.Outline);
                    dpo.visuals[1] = new PathStepVisual(path);
                }

            }
        }
        
        
    }
}
