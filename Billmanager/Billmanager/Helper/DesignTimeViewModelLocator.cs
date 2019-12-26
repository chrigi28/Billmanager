using Billmanager.ViewModels;

namespace Billmanager.Helper
{
    public class DesignTimeViewModelLocator
    {
        public static MainPageViewModel MainPageViewModel => null;
        public static OverviewPageViewModel OverviewPageViewModel => new OverviewPageViewModel(null);
        public static CreateAddresscardPageViewModel CreateAddresscardPageViewModel => new CreateAddresscardPageViewModel(null);
        public static CreateCarPageViewModel CreateCarPageViewModel => new CreateCarPageViewModel(null);
        public static CreateBillPageViewModel CreateBillPageViewModel => new CreateBillPageViewModel(null);
        public static CreateCustomerPageViewModel CreateCustomerPageViewModel => new CreateCustomerPageViewModel(null);
        public static CreateOffertPageViewModel CreateOffertPageViewModel => new CreateOffertPageViewModel(null);
        public static CreateWorkcardPageViewModel CreateWorkcardPageViewModel => new CreateWorkcardPageViewModel(null);
        public static CustomerSelectionPageViewModel CustomerSelectionPageViewModel => new CustomerSelectionPageViewModel(null);
        

        ////public static SelectionPageViewModel SelectionPageViewModel => new SelectionPageViewModel<null>(null);
    }
}