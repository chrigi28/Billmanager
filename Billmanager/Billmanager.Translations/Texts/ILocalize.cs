using System.Globalization;

namespace Billmanager.Translations.Texts;

/// <summary>
/// Interface ILocalize
/// </summary>
public interface ILocalize
{
    /// <summary>
    /// Gets the current culture information.
    /// </summary>
    /// <returns>CultureInfo.</returns>
    CultureInfo GetCurrentCultureInfo();

    /// <summary>
    /// Sets the locale.
    /// </summary>
    /// <param name="ci">The ci.</param>
    void SetLocale(CultureInfo ci);
}