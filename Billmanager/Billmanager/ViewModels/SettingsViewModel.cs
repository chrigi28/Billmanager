using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Billmanager.Database.Tables;
using Billmanager.Helper;
using Billmanager.Interface.ViewModels;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Billmanager.ViewModels;

public class SettingViewModel : DataViewModelBase<SettingsDbt>, ISettingsViewModel<SettingsDbt>
{
    private Command pickImgeCommand;

    public SettingViewModel(INavigationService? ns) : base(ns)
    {
        this.Title = Resources.CreateCustomer;
    }

    public Command PickImageCommand => this.pickImgeCommand ??= new Command(async () => await this.PickAndShow());

    async Task PickAndShow()
    {
        try
        {
            var result = await FilePicker.PickAsync(PickOptions.Images);
            if (result != null)
            {
                var path = await DependencyService.Get<ICopyDataToLocalStorageService>().CopyDataToLocalStore(result);
                Model.LogoPath = path;
            }
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }
    }

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        var settings = await DependencyService.Get<IBaseService>().GetAllAsync<SettingsDbt>();
        var setting = settings.FirstOrDefault();
        if (setting != null)
        {
            this.Model = setting;
        }
        else
        {
            this.Model = new SettingsDbt();
        }

    }
}