using Billmanager.Database.Tables;
using Billmanager.Interfaces;
using Billmanager.Services;
using Prism;
using Prism.Ioc;
using Billmanager.ViewModels;
using Billmanager.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Billmanager
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SharedInitializations.Initialize();
            await NavigationService.NavigateAsync("NavigationPage/Overview");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Overview, OverviewViewModel>();
            containerRegistry.RegisterForNavigation<CreateCustomer, CreateCustomerViewModel>();
            containerRegistry.RegisterForNavigation<CreateCar, CreateCarViewModel>();
            containerRegistry.RegisterForNavigation<CreateBill, CreateBillViewModel>();
            containerRegistry.RegisterForNavigation<CreateWorkcard, CreateWorkcardViewModel>();
            containerRegistry.RegisterForNavigation<CreateAddresscard, CreateAddresscardViewModel>();
            containerRegistry.RegisterForNavigation<CreateOffert, CreateOffertViewModel>();
            containerRegistry.RegisterForNavigation<SelectionPage, SelectionPageViewModel<CustomerDbt>>("CustomerSelection");
            containerRegistry.RegisterForNavigation<SelectionPage, SelectionPageViewModel<CarDbt>>("CarSelection");
        }
    }
}
