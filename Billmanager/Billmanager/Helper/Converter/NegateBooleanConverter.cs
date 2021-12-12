using System;

namespace Muellerchur.Xamos.Mobile.Converters;

/// <summary>Negate Boolean Converter</summary>
public sealed class NegateBooleanConverter : ConverterBase
{
    /// <summary>Convert</summary>
    /// <param name="value">value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">parameter</param>
    /// <param name="language">language</param>
    /// <returns>negated boolean</returns>
    public override object Convert(object? value, Type targetType, object? parameter, string language)
    {
        if (value is bool val)
        {
            return !val;
        }

        return true;
    }

    /// <summary>Convert Back</summary>
    /// <param name="value">value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">parameter</param>
    /// <param name="language">language</param>
    /// <returns>negated boolean</returns>
    public override object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return !(bool)value;
    }
}