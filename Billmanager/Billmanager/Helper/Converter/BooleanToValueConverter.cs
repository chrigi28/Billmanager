using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Muellerchur.Xamos.Mobile.Converters
{
    /// <summary>Class BooleanOpacityConverter. This class cannot be inherited.</summary>
    /// <seealso cref="IMarkupExtension" />
    /// <seealso cref="IValueConverter" />
    public sealed class BooleanToValueConverter : BindableObject, IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// Bindable True value
        /// </summary>
        public static readonly BindableProperty TrueValueProperty = BindableProperty.Create(nameof(TrueValue), typeof(object), typeof(object));

        /// <summary>
        /// Bindable True value
        /// </summary>
        public static readonly BindableProperty FalseValueProperty = BindableProperty.Create(nameof(FalseValue), typeof(object), typeof(object));

        /// <summary>
        /// Gets or sets the true text
        /// </summary>
        public object TrueValue
        {
            get => this.GetValue(BooleanToValueConverter.TrueValueProperty);

            set => this.SetValue(BooleanToValueConverter.TrueValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the false text
        /// </summary>
        public object FalseValue
        {
            get => this.GetValue(BooleanToValueConverter.FalseValueProperty);

            set => this.SetValue(BooleanToValueConverter.FalseValueProperty, value);
        }

        /// <summary>Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>To be added.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool trueValue)
            {
                if (trueValue)
                {
                    return this.TrueValue;
                }
            }

            return this.FalseValue;
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