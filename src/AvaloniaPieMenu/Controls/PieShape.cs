using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Media;

namespace AvaloniaPieMenu.Controls;

/// <summary>
/// Interaction logic for PieShape.xaml
/// </summary>
internal class PieShape : Shape
{
    public PieShape()
    {
        
    }
    public static readonly StyledProperty<  double> InnerRadiusProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(InnerRadius),defaultBindingMode:BindingMode.TwoWay);


    /// <summary>
    /// The inner radius of this pie piece
    /// </summary>
    public double InnerRadius
    {
        get { return (double)GetValue(InnerRadiusProperty); }
        set { SetValue(InnerRadiusProperty, value); }
    }



    public static readonly StyledProperty<double> OuterRadiusProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(OuterRadius),defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The outer radius of this pie piece
    /// </summary>
    public double OuterRadius
    {
        get { return (double)GetValue(OuterRadiusProperty); }
        set { SetValue(OuterRadiusProperty, value); }
    }

    public static readonly StyledProperty<double> PaddingProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(Padding),defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The padding arround this pie piece
    /// </summary>
    public double Padding
    {
        get { return (double)GetValue(PaddingProperty); }
        set { SetValue(PaddingProperty, value); }
    }



    public static readonly StyledProperty<  double> PushOutProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(PushOut),defaultBindingMode:BindingMode.TwoWay);
    /// <summary>
    /// The distance to 'push' this pie piece out from the center
    /// </summary>
    public double PushOut
    {
        get { return (double)GetValue(PushOutProperty); }
        set { SetValue(PushOutProperty, value); }
    }

    public static readonly StyledProperty<double> AngleDeltaProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(AngleDelta),defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The angle delta of this pie piece in degrees
    /// </summary>
    public double AngleDelta
    {
        get { return (double)GetValue(AngleDeltaProperty); }
        set { SetValue(AngleDeltaProperty, value); }
    }

    public static readonly StyledProperty< double> StartAngleProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(StartAngle),defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The start angle from the Y axis vector of this pie piece in degrees
    /// </summary>
    public double StartAngle
    {
        get { return (double)GetValue(StartAngleProperty); }
        set { SetValue(StartAngleProperty, value); }
    }

    public static readonly StyledProperty<double> CenterXProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(CenterX),defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The X coordinate of center of the circle from which this pie piece is cut
    /// </summary>
    public double CenterX
    {
        get { return (double)GetValue(CenterXProperty); }
        set { SetValue(CenterXProperty, value); }
    }

    public static readonly StyledProperty<double> CenterYProperty =
        AvaloniaProperty.Register<PieShape, double>(
            nameof(CenterY),defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The Y coordinate of center of the circle from which this pie piece is cut
    /// </summary>
    public double CenterY
    {
        get { return (double)GetValue(CenterYProperty); }
        set { SetValue(CenterYProperty, value); }
    }

    static PieShape()
    {

    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == AngleDeltaProperty)
        {
            this.InvalidateGeometry();
        }
    }

    protected override Geometry? CreateDefiningGeometry()
    {
        //Creates a StreamGeometry for describing the shape

        PathGeometry geometry = new PathGeometry();
          
        geometry.FillRule = FillRule.EvenOdd;

        using (StreamGeometryContext context = geometry.Open())
        {
            DrawGeometry(context);
        }


        // Freezes the geometry for performance benefits
        //geometry.Freeze();

        this.RenderTransform = new RotateTransform(StartAngle);
        this.RenderTransformOrigin = new RelativePoint(CenterX, CenterY,RelativeUnit.Absolute);
        return geometry;
    }
    /// <summary>
    /// Defines the shape
    /// </summary>
    //protected override Geometry DefiningGeometry
    //{
    //    get
    //    {
    //        // Creates a StreamGeometry for describing the shape
    //        StreamGeometry geometry = new StreamGeometry();
    //        geometry.FillRule = FillRule.EvenOdd;

    //        using (StreamGeometryContext context = geometry.Open())
    //        {
    //            DrawGeometry(context);
    //        }

    //        // Freezes the geometry for performance benefits
    //        geometry.Freeze();

    //        return geometry;
    //    }
    //}

    /// <summary>
    /// Draws the pie piece
    /// </summary>
    private void DrawGeometry(StreamGeometryContext context)
    {

        if (AngleDelta <= 0)
        {
            return;
        }

        double outerStartAngle = 0;
        double outerAngleDelta = AngleDelta;
        double innerStartAngle = 0;
        double innerAngleDelta = AngleDelta;
        Point arcCenter = new Point(CenterX, CenterY);
        Size outerArcSize = new Size(OuterRadius, OuterRadius);
        Size innerArcSize = new Size(InnerRadius, InnerRadius);

        // If have to draw a full-circle, draws two semi-circles, because 'ArcTo()' can not draw a full-circle
        if (AngleDelta >= 360 && Padding <= 0)
        {
            Point outerArcTopPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle, OuterRadius + PushOut);
            Point outerArcBottomPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle + 180, OuterRadius + PushOut);
            Point innerArcTopPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle, InnerRadius + PushOut);
            Point innerArcBottomPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle + 180, InnerRadius + PushOut);

            context.BeginFigure(innerArcTopPoint, true);
            context.LineTo(outerArcTopPoint, true);
            context.ArcTo(outerArcBottomPoint, outerArcSize, 0, false, SweepDirection.Clockwise, true);
            context.ArcTo(outerArcTopPoint, outerArcSize, 0, false, SweepDirection.Clockwise, true);
            context.LineTo(innerArcTopPoint, true);
            context.ArcTo(innerArcBottomPoint, innerArcSize, 0, false, SweepDirection.CounterClockwise, true);
            context.ArcTo(innerArcTopPoint, innerArcSize, 0, false, SweepDirection.CounterClockwise, true);
        }
        // Else draws as always
        else
        {
            if (Padding > 0)
            {
                // Offsets the angle by the padding
                double outerAngleVariation = (180 * (Padding / OuterRadius)) / Math.PI;
                double innerAngleVariation = (180 * (Padding / InnerRadius)) / Math.PI;

                outerStartAngle += outerAngleVariation;
                outerAngleDelta -= outerAngleVariation * 2;
                innerStartAngle += innerAngleVariation;
                innerAngleDelta -= innerAngleVariation * 2;
            }

            Point outerArcStartPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle, OuterRadius + PushOut);
            Point outerArcEndPoint = ComputeCartesianCoordinate(arcCenter, outerStartAngle + outerAngleDelta, OuterRadius + PushOut);
            Point innerArcStartPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle, InnerRadius + PushOut);
            Point innerArcEndPoint = ComputeCartesianCoordinate(arcCenter, innerStartAngle + innerAngleDelta, InnerRadius + PushOut);

            bool largeArcOuter = outerAngleDelta > 180.0;
            bool largeArcInner = innerAngleDelta > 180.0;

            context.BeginFigure(innerArcStartPoint, true);
            context.LineTo(outerArcStartPoint, true);
            context.ArcTo(outerArcEndPoint, outerArcSize, 0, largeArcOuter, SweepDirection.Clockwise, true);
            context.LineTo(innerArcEndPoint, true);
            context.ArcTo(innerArcStartPoint, innerArcSize, 0, largeArcInner, SweepDirection.CounterClockwise, true);
        }
    }

    private static Point ComputeCartesianCoordinate(Point center, double angle, double radius)
    {
        // Converts to radians
        double radiansAngle = (Math.PI / 180.0) * (angle - 90);
        double x = radius * Math.Cos(radiansAngle);
        double y = radius * Math.Sin(radiansAngle);
        return new Point(x + center.X, y + center.Y);
    }
}