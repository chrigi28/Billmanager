using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Muellerchur.Xamos.Mobile.Converters;

/// <summary> Converter Base Class </summary>
public class ConverterBase : IValueConverter, IMarkupExtension
{
    /// <summary> Provide Value </summary>
    /// <param name="serviceProvider">Service Provider</param>
    /// <returns>this item</returns>
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    /// <summary> Convert  </summary>
    /// <param name="value">value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">parameter</param>
    /// <param name="culture">culture</param>
    /// <returns>converted value</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return this.Convert(value, targetType, parameter, culture.Name);
    }

    /// <summary> Convert  </summary>
    /// <param name="value">value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">parameter</param>
    /// <param name="language">culture</param>
    /// <returns>converted value</returns>
    public virtual object? Convert(object? value, Type targetType, object? parameter, string language)
    {
        return value;
    }

    /// <summary> Convert Back </summary>
    /// <param name="value">value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">parameter</param>
    /// <param name="culture">culture</param>
    /// <returns>back converted value</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return this.ConvertBack(value, targetType, parameter, culture.Name);
    }

    /// <summary> Convert  </summary>
    /// <param name="value">value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">parameter</param>
    /// <param name="language">culture</param>
    /// <returns>converted value</returns>
    public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}