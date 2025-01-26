using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace AvaloniaPieMenu.Controls
{
    /// <summary>
    /// Interaction logic for RadialMenuItem.xaml
    /// </summary>
    public class RadialMenuItem : Button
    {
        public static readonly StyledProperty<int> IndexProperty =
            AvaloniaProperty.Register<RadialMenuItem, int>(
                nameof(Index),
                defaultBindingMode: BindingMode.TwoWay);

        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set
            {
                SetValue(IndexProperty, value); 
                UpdateItemRendering();
            }
        }

        public static readonly StyledProperty<  int> CountProperty =
            AvaloniaProperty.Register<RadialMenuItem, int>(
                nameof(Count),defaultBindingMode:BindingMode.TwoWay);

        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set
            {
                SetValue(CountProperty, value); 
                UpdateItemRendering();
            }
        }

        public static readonly StyledProperty< bool> HalfShiftedProperty =
            AvaloniaProperty.Register<RadialMenuItem, bool>(
                nameof(HalfShifted),defaultBindingMode:BindingMode.TwoWay);

        public bool HalfShifted
        {
            get { return (bool)GetValue(HalfShiftedProperty); }
            set
            {
                SetValue(HalfShiftedProperty, value);
                UpdateItemRendering();
            }
        }

        public static readonly StyledProperty<  double> CenterXProperty =
            AvaloniaProperty.Register< RadialMenuItem, double>(
                nameof(CenterX),defaultBindingMode:BindingMode.TwoWay);

        public double CenterX
        {
            get { return (double)GetValue(CenterXProperty); }
            set { SetValue(CenterXProperty, value); }
        }

        public static readonly StyledProperty< double> CenterYProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(CenterY), defaultBindingMode: BindingMode.TwoWay);

        public double CenterY
        {
            get { return (double)GetValue(CenterYProperty); }
            set { SetValue(CenterYProperty, value); }
        }

        public static readonly StyledProperty<double> OuterRadiusProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(OuterRadius), defaultBindingMode: BindingMode.TwoWay);

        public double OuterRadius
        {
            get { return (double)GetValue(OuterRadiusProperty); }
            set { SetValue(OuterRadiusProperty, value); }
        }

        public static readonly StyledProperty<double> InnerRadiusProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(InnerRadius), defaultBindingMode: BindingMode.TwoWay);

        public double InnerRadius
        {
            get { return (double)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        public static readonly StyledProperty<double> PaddingProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(Padding),
                defaultBindingMode:BindingMode.TwoWay);

        public new double Padding
        {
            get { return (double)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public static readonly StyledProperty<double> ContentRadiusProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(ContentRadius), defaultBindingMode: BindingMode.TwoWay);

        public double ContentRadius
        {
            get { return (double)GetValue(ContentRadiusProperty); }
            set { SetValue(ContentRadiusProperty, value); }
        }

        public static readonly StyledProperty<double> EdgeOuterRadiusProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(EdgeOuterRadius), defaultBindingMode: BindingMode.TwoWay);

        public double EdgeOuterRadius
        {
            get { return (double)GetValue(EdgeOuterRadiusProperty); }
            set { SetValue(EdgeOuterRadiusProperty, value); }
        }

        public static readonly StyledProperty< double> EdgeInnerRadiusProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(EdgeInnerRadius), defaultBindingMode: BindingMode.TwoWay);

        public double EdgeInnerRadius
        {
            get { return (double)GetValue(EdgeInnerRadiusProperty); }
            set { SetValue(EdgeInnerRadiusProperty, value); }
        }

        public static readonly StyledProperty<double> EdgePaddingProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(EdgePadding), defaultBindingMode: BindingMode.TwoWay);

        public double EdgePadding
        {
            get { return (double)GetValue(EdgePaddingProperty); }
            set { SetValue(EdgePaddingProperty, value); }
        }

        public static readonly StyledProperty<IImmutableBrush> EdgeBackgroundProperty =
            AvaloniaProperty.Register<RadialMenuItem, IImmutableBrush>(
                nameof(EdgeBackground), defaultBindingMode: BindingMode.TwoWay);

        public IImmutableBrush EdgeBackground
        {
            get { return (IImmutableBrush)GetValue(EdgeBackgroundProperty); }
            set { SetValue(EdgeBackgroundProperty, value); }
        }

        public static readonly StyledProperty<double> EdgeBorderThicknessProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(EdgeBorderThickness), defaultBindingMode: BindingMode.TwoWay);

        public double EdgeBorderThickness
        {
            get { return (double)GetValue(EdgeBorderThicknessProperty); }
            set { SetValue(EdgeBorderThicknessProperty, value); }
        }

        public static readonly StyledProperty<  Brush> EdgeBorderBrushProperty =
            AvaloniaProperty.Register<RadialMenuItem, Brush>(
                nameof(EdgeBorderBrush),defaultBindingMode:BindingMode.TwoWay);

        public Brush EdgeBorderBrush
        {
            get { return (Brush)GetValue(EdgeBorderBrushProperty); }
            set { SetValue(EdgeBorderBrushProperty, value); }
        }

        public static readonly StyledProperty< IImmutableBrush> ArrowBackgroundProperty =
            AvaloniaProperty.Register<RadialMenuItem, IImmutableBrush>(
                nameof(ArrowBackground), defaultBindingMode: BindingMode.TwoWay);

        public IImmutableBrush ArrowBackground
        {
            get { return (IImmutableBrush)GetValue(ArrowBackgroundProperty); }
            set { SetValue(ArrowBackgroundProperty, value); }
        }

        public static readonly StyledProperty<double> ArrowBorderThicknessProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(ArrowBorderThickness), defaultBindingMode: BindingMode.TwoWay);

        public double ArrowBorderThickness
        {
            get { return (double)GetValue(ArrowBorderThicknessProperty); }
            set { SetValue(ArrowBorderThicknessProperty, value); }
        }

        public static readonly StyledProperty<Brush> ArrowBorderBrushProperty =
            AvaloniaProperty.Register<RadialMenuItem, Brush>(
                nameof(ArrowBorderBrush), defaultBindingMode: BindingMode.TwoWay);

        public Brush ArrowBorderBrush
        {
            get { return (Brush)GetValue(ArrowBorderBrushProperty); }
            set { SetValue(ArrowBorderBrushProperty, value); }
        }

        public static readonly StyledProperty <double> ArrowWidthProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(ArrowWidth), defaultBindingMode: BindingMode.TwoWay);

        public double ArrowWidth
        {
            get { return (double)GetValue(ArrowWidthProperty); }
            set { SetValue(ArrowWidthProperty, value); }
        }

        public static readonly StyledProperty< double> ArrowHeightProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(ArrowHeight), defaultBindingMode: BindingMode.TwoWay);

        public double ArrowHeight
        {
            get { return (double)GetValue(ArrowHeightProperty); }
            set { SetValue(ArrowHeightProperty, value); }
        }

        public static readonly StyledProperty<double> ArrowRadiusProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(ArrowRadius), defaultBindingMode: BindingMode.TwoWay);

        public double ArrowRadius
        {
            get { return (double)GetValue(ArrowRadiusProperty); }
            set { SetValue(ArrowRadiusProperty, value); }
        }

        public static readonly StyledProperty<double> AngleDeltaProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(AngleDelta), defaultBindingMode: BindingMode.OneWayToSource);

        public double AngleDelta
        {
            get { return (double)GetValue(AngleDeltaProperty); }
            protected set { SetValue(AngleDeltaProperty, value); }
        }

        public static readonly StyledProperty<double> StartAngleProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(StartAngle), defaultBindingMode: BindingMode.TwoWay);

        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            protected set { SetValue(StartAngleProperty, value); }
        }

        public static readonly StyledProperty<  double> RotationProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(Rotation), defaultBindingMode: BindingMode.TwoWay);

        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            protected set { SetValue(RotationProperty, value); }
        }

        public static readonly StyledProperty<object?> IconProperty =
            AvaloniaProperty.Register<RadialMenuItem, object?>(
                nameof(Icon), defaultBindingMode: BindingMode.TwoWay);

        public object? Icon
        {
            get { return (object?)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public ObservableCollection<RadialMenuItem> SubMenuItems { get; private set; }=new ObservableCollection<RadialMenuItem>();
        
        static RadialMenuItem()
        {
            
        }
        public static readonly StyledProperty<RadialMenuItem?> ParentMenuItemProperty =
            AvaloniaProperty.Register<RadialMenuItem, RadialMenuItem?>(
                nameof(Rotation), defaultBindingMode: BindingMode.TwoWay);

        public RadialMenuItem? ParentMenuItem
        {
            get { return (RadialMenuItem?)GetValue(ParentMenuItemProperty); }
            set { SetValue(ParentMenuItemProperty, value); }
        }
        public static readonly StyledProperty<double> ContentRotationProperty =
            AvaloniaProperty.Register<RadialMenuItem, double>(
                nameof(ContentRotation), defaultBindingMode: BindingMode.OneWay);

        public double ContentRotation
        {
            get { return (double)GetValue(ContentRotationProperty); }
            protected set { SetValue(ContentRotationProperty, value); }
        }

        private void UpdateItemRendering()
        {
            if(this.Count==0)
                return;
            var angleDelta = 360.0 / this.Count;
            var angleShift = this.HalfShifted ? -angleDelta / 2 : 0;
            var startAngle = angleDelta * this.Index + angleShift;
            var rotation = startAngle + (angleDelta / 2d);

            this.AngleDelta = angleDelta;
            this.StartAngle = startAngle;
            this.Rotation = rotation;
            ContentRotation= rotation + 180;
            Debug.WriteLine($"item{Index} : bounds:{Bounds}");
            
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
             

        }

        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            
            base.OnSizeChanged(e);
            
            
            //Debug.WriteLine($"item{Index} : bounds:{Bounds}");
        }

        public RadialMenuItem()
        {
            
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
           
            base.OnLoaded(e);


            var r = Rotation;
            Rotation = -5;
            Rotation = r;
            UpdateItemRendering();
        }

        protected override void OnClick()
        {
            var rm = this.GetVisualAncestors().FirstOrDefault(u => u is RadialMenu rm) as RadialMenu;
            if (rm != null && this.SubMenuItems.Any())
            {
                rm.SelectMenuItem(this);
            }
            base.OnClick();
        }

         
    }
}
