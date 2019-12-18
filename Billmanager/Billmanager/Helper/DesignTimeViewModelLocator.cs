using Billmanager.ViewModels;

namespace Billmanager.Helper
{
    public class DesignTimeViewModelLocator
    {
        public static MainPageViewModel MainPageViewModel => null;
        public static OverviewViewModel OverviewViewModel => new OverviewViewModel(null);
        public static CreateAddresscardViewModel CreateAddresscardViewModel => new CreateAddresscardViewModel(null);
        public static CreateCarViewModel CreateCarViewModel => new CreateCarViewModel(null);
        public static CreateBillViewModel CreateBillViewModel => new CreateBillViewModel(null);
        public static CreateCustomerViewModel CreateCustomerViewModel => new CreateCustomerViewModel(null);
        public static CreateOffertViewModel CreateOffertViewModel => new CreateOffertViewModel(null);
        public static CreateWorkcardViewModel CreateWorkcardViewModel => new CreateWorkcardViewModel(null);
        ////public static SelectionPageViewModel SelectionPageViewModel => new SelectionPageViewModel<null>(null);
    }
}