using System.Globalization;
using Billmanager.Interfaces;
using Billmanager.Interfaces.Database;
using Billmanager.Translations.Texts;
using Microsoft.EntityFrameworkCore.Query;
using Xamarin.Forms;

namespace Billmanager.Services
{
    public static class SharedInitializations
    {
        public static void Initialize()
        {
            DependencyService.Register<IDbPath, IDbPath>();
        }

        public static void PreInit()
        {
            Resources.Culture = new CultureInfo("de-CH");
        }
    }
}