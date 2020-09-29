using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Muellerchur.Xamos.Mobile.Converters
{
    /// <summary>Class BooleanOpacityConverter. This class cannot be inherited.</summary>
    /// <seealso cref="IMarkupExtension" />
    /// <seealso cref="IValueConverter" />
    public sealed class BooleanOpacityConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// Gets or sets a value indicating whether to invert the bool
        /// </summary>
        public bool InvertedBoolean { get; set; } = false;

        /// <summary>Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>To be added.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool enabled)
            {
                if (this.InvertedBoolean)
                {
                    enabled = !enabled;
                }

                if (!enabled)
                {
                    return 0.5;
                }
            }

            return 1;
        }

        /// <summary>Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>To be added.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        /// <summary>Returns the object created from the markup extension.</summary>
        /// <param name="serviceProvider">The service that provides the value.</param>
        /// <returns>The object</returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}