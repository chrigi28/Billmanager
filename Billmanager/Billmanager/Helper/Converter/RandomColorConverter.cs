using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Muellerchur.Xamos.Mobile.Converters
{
    /// <summary>Class ErrorToBackgroundColorConverter. This class cannot be inherited.</summary>
    /// <seealso cref="IMarkupExtension" />
    /// <seealso cref="IValueConverter" />
    public sealed class RandomColorConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// default colors
        /// </summary>
        private static readonly List<Color> Cols = new List<Color>
        {
            Color.Lime,
            Color.Aqua,
            Color.Red,
            Color.Orange,
            Color.Beige,
            Color.Green,
            Color.Blue,
            Color.Brown,
            Color.DarkSlateGray,
            Color.Purple,
            Color.HotPink,
            Color.BlueViolet,
            Color.DarkRed,
            Color.Turquoise
        };

        /// <summary>Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>To be added.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ProvideValue(null);
        }

        /// <summary>Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>To be added.</returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        /// <summary>Returns the object created from the markup extension.</summary>
        /// <param name="serviceProvider">The service that provides the value.</param>
        /// <returns>The object</returns>
        public object ProvideValue(IServiceProvider? serviceProvider)
        {
            return RandomColorConverter.Cols[new Random().Next(RandomColorConverter.Cols.Count)];
        }
    }
}