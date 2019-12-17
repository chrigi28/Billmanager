using System;

namespace Billmanager.Translations.Texts
{
    /// <summary>
    /// Class PlatformCulture.
    /// </summary>
    public sealed class PlatformCulture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformCulture"/> class.
        /// </summary>
        /// <param name="platformCultureString">The platform culture string.</param>
        /// <exception cref="ArgumentException">Expected culture identifier - platformCultureString</exception>
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException(
                    Resources.Expected_culture_identifier,
                    nameof(platformCultureString)); // in C# 6 use nameof(platformCultureString)
            }

            this.PlatformString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore
            var dashIndex = this.PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = this.PlatformString.Split('-');
                this.LanguageCode = parts[0];
                this.LocaleCode = parts[1];
            }
            else
            {
                this.LanguageCode = this.PlatformString;
                this.LocaleCode = string.Empty;
            }
        }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string LanguageCode { get; }

        /// <summary>
        /// Gets the locale code.
        /// </summary>
        /// <value>The locale code.</value>
        public string LocaleCode { get; }

        /// <summary>
        /// Gets the platform string.
        /// </summary>
        /// <value>The platform string.</value>
        public string PlatformString { get; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.PlatformString;
        }
    }
}