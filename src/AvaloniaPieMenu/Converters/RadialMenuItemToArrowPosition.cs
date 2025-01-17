﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;

namespace AvaloniaPieMenu.Converters;

internal class RadialMenuItemToArrowPosition : IMultiValueConverter
{
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("RadialMenuItemToArrowPosition is a One-Way converter only !");
    }

    private static Point ComputeCartesianCoordinate(Point center, double angle, double radius)
    {
        // Converts to radians
        double radiansAngle = (Math.PI / 180.0) * (angle - 90);
        double x = radius * Math.Cos(radiansAngle);
        double y = radius * Math.Sin(radiansAngle);
        return new Point(x + center.X, y + center.Y);
    }

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 5)
        {
            throw new ArgumentException("RadialMenuItemToArrowPosition converter needs 7 values (double centerX, double centerY, double arrowWidth, double arrowHeight, double arrowRadius) !", "values");
        }
        if (parameter == null)
        {
            throw new ArgumentNullException("parameter", "RadialMenuItemToArrowPosition converter needs the parameter (string axis) !");
        }

        string axis = (string)parameter;

        if (axis != "X" && axis != "Y")
        {
            throw new ArgumentException("RadialMenuItemToArrowPosition parameter needs to be 'X' or 'Y' !", "parameter");
        }

        if (values.Any(u => u.GetType() == typeof(UnsetValueType)))
        {
            return null;}
            //throw new ArgumentException("RadialMenuItemToArrowPosition converter needs 7 values (double centerX, double centerY, double arrowWidth, double arrowHeight, double arrowRadius) !", "values");

        double centerX = (double)values[0];
        double centerY = (double)values[1];
        double arrowWidth = (double)values[2];
        double arrowHeight = (double)values[3];
        double arrowRadius = (double)values[4];

        if (axis == "X")
        {
            return centerX - (arrowWidth / 2);
        }

        return centerY - arrowRadius - (arrowHeight / 2);
    }
}