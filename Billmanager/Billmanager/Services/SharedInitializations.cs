using System.Globalization;
using Billmanager.Interfaces.Database;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Microsoft.EntityFrameworkCore.Query;
using Xamarin.Forms;

namespace Billmanager.Services
{
    public static class SharedInitializations
    {
        public static void Initialize()
        {
            DependencyService.Register<IDbPath>();
            DependencyService.Register<ISelectionData>();

            InitializeServices();
        }

        public static void PreInit()
        {
            Resources.Culture = new CultureInfo("de-CH");
        }

        private static void InitializeServices()
        {
            DependencyService.Register<ICustomerService>();
            DependencyService.Register<ICarService>();
            DependencyService.Register<IBillService>();
        }
    }
}