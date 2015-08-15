using Geometry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Geometry.Elements
{
    public class PathElementOld : FrameworkElement
    {

        private PathOutlineVisualOld _visual;
        private PathOld path;

        public PathElementOld()
        {
            path = new PathOld();
            _visual = new PathOutlineVisualOld(path, Colors.Black);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visual;
        }

        public int PathWidth
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
        		return (int)GetValue(PathWidthProperty);
        	}
        	set
        	{
        		SetValue(PathWidthProperty, value);
        	}
        }
        public int Diameter
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
        		return (int)GetValue(DiameterProperty);
        	}
        	set
        	{
        		SetValue(DiameterProperty, value);
        	}
        }
        public int EndY
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
        		return (int)GetValue(EndYProperty);
        	}
        	set
        	{
        		SetValue(EndYProperty, value);
        	}
        }
        public int EndX
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
        		return (int)GetValue(EndXProperty);
        	}
        	set
        	{
        		SetValue(EndXProperty, value);
        	}
        }
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public int StartY
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
        		return (int)GetValue(StartYProperty);
        	}
        	set
        	{
        		SetValue(StartYProperty, value);
        	}
        }
        public int StartX
        {
        	// IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        	get
        	{
                return (int)GetValue(StartXProperty);
            }
            set
            {
                SetValue(StartXProperty, value);
            }
        }

        public static readonly DependencyProperty StartXProperty = 
            DependencyProperty.Register("StartX", typeof(int), typeof(PathElementOld), 
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnStartXChanged)));

        private static void OnStartXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dob = d as PathElementOld;

            if (dob != null)
            {
                dob.path.StartPoint = new Point(dob.StartX, dob.path.StartPoint.Y);
                dob._visual = new PathOutlineVisualOld(dob.path, Colors.Black);
            }
        }
        
        public static readonly DependencyProperty StartYProperty = 
            DependencyProperty.Register("StartY", typeof(int), typeof(PathElementOld), 
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnStartYChanged)));

        private static void OnStartYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dob = d as PathElementOld;

            if (dob != null)
            {
                dob.path.StartPoint = new Point(dob.path.StartPoint.X, dob.StartY);
                dob._visual = new PathOutlineVisualOld(dob.path, Colors.Black);
            }
        }
        
        public static readonly DependencyProperty EndXProperty = 
            DependencyProperty.Register("EndX", 
                typeof(int), 
                typeof(PathElementOld), 
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnEndXChanged)));

        private static void OnEndXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dob = d as PathElementOld;

            if (dob != null)
            {
                dob.path.EndPoint = new Point(dob.EndX, dob.path.EndPoint.Y);
                dob._visual = new PathOutlineVisualOld(dob.path, Colors.Black);
            }
        }
        
        public static readonly DependencyProperty EndYProperty = 
            DependencyProperty.Register("EndY", 
                typeof(int), 
                typeof(PathElementOld), 
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnEndYChanged)));

        private static void OnEndYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dob = d as PathElementOld;

            if (dob != null)
            {
                dob.path.EndPoint = new Point(dob.path.EndPoint.X, dob.EndY);
                dob._visual = new PathOutlineVisualOld(dob.path, Colors.Black);
            }
        }
        
        public static readonly DependencyProperty DiameterProperty = 
            DependencyProperty.Register("Diameter", 
                typeof(int), 
                typeof(PathElementOld), 
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnDiameterChanged)));

        private static void OnDiameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dob = d as PathElementOld;

            if (dob != null)
            {
                dob.path.Diameter = dob.Diameter;
                dob._visual = new PathOutlineVisualOld(dob.path, Colors.Black);
            }
        }
        
        public static readonly DependencyProperty PathWidthProperty = 
            DependencyProperty.Register("PathWidth", 
                typeof(int), 
                typeof(PathElementOld), 
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnWidthChanged)));
        
        

        private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dob = d as PathElementOld;

            if (dob != null)
            {
                dob.path.Width = dob.PathWidth;
                dob._visual = new PathOutlineVisualOld(dob.path, Colors.Black);
            }
        }
        
        
    }
}
