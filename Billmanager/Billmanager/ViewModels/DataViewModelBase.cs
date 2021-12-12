using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interfaces.Database;
using Prism.Navigation;
using Xamarin.Forms;

namespace Billmanager.ViewModels;

/// <summary> The data view Model base. </summary>
public class DataViewModelBase<T> : ViewModelBase where T : class, IDatabaseTable, new()
{
    private Command saveCommand;

    public DataViewModelBase(INavigationService? navigationService) : base(navigationService)
    {
        this.Model = new T();
    }

    public Command SaveCommand => this.saveCommand ?? (this.saveCommand = new Command(async () => await this.Save()));

    public T Model { get; set; }

    /// <summary>If Id of item is empty => new add otherwise update</summary>
    public virtual async Task Save(bool goBack = true)
    {
        if (this.Model.Id == 0)
        {

            await SqliteDatabase.AssureDb().AddItemAsync(this.Model).ConfigureAwait(false);

        }
        else
        {
            await SqliteDatabase.AssureDb().UpdateItemAsync(this.Model).ConfigureAwait(false);

        }

        if (goBack)
        {
            await NavigationService.GoBackAsync();
        }
    }
}