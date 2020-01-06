using System.Threading.Tasks;
using Billmanager.Interfaces.Database;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Xamarin.Forms;

namespace Billmanager.Interface.ViewModels
{
    /// <summary> The data view Model base. </summary>
    public interface IDataViewModelBase<T> : IViewModelBase
    {
        Command SaveCommand { get; }

        T Model { get; set; }

        /// <summary>If Id of item is empty => new add otherwise update</summary>
        Task Save(bool goBack = true);
    }
}