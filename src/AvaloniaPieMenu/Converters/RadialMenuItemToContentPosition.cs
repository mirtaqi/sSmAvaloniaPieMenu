using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data.Converters;
using Avalonia.VisualTree;
using AvaloniaPieMenu.Controls;

namespace AvaloniaPieMenu.Converters;

internal class RadialMenuItemToContentPosition : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count !=7)
        {
            throw new ArgumentException("RadialMenuItemToContentPosition converter needs 6 values (double angle, double centerX, double centerY, double contentWidth, double contentHeight, double contentRadius) !", "values");
        }
        if (parameter == null)
        {
            throw new ArgumentNullException("parameter", "RadialMenuItemToContentPosition converter needs the parameter (string axis) !");
        }

        string axis = (string)parameter;

        if (axis != "X" && axis != "Y")
        {
            throw new ArgumentException("RadialMenuItemToContentPosition parameter needs to be 'X' or 'Y' !", "parameter");
        }
        if (values.Any(u => u is null || u.GetType() == typeof(UnsetValueType)))
        {
            return null;
        }
        double angle = (double)values[0];
        double centerX = (double)values[1];
        double centerY = (double)values[2];
        double contentWidth = (double)values[3];
        double contentHeight = (double)values[4];
        double contentRadius = (double)values[5];
        if (values[6] is RadialMenuItem mi)
        {
            var c = mi.GetVisualDescendants().FirstOrDefault(u => u.Name == "PART_RootContainer") as Control;
            if (c is not null )
            {
                if (c.Bounds.Width > 0 && c.Bounds.Height > 0)
                {
                    
                    contentWidth = c.Bounds.Width;
                    contentHeight = c.Bounds.Height;
                }
            }
        }

       

        Point contentPosition = ComputeCartesianCoordinate(new Point(centerX, centerY), angle, contentRadius);

        if (axis == "X")
        {
            return contentPosition.X - (contentWidth/4 );
        }

        return contentPosition.Y - (contentHeight/4 );
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("RadialMenuItemToContentPosition is a One-Way converter only !");
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