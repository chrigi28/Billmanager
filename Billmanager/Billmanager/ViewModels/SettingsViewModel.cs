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

namespace Billmanager.ViewModels
{
    public class SettingViewModel : DataViewModelBase<SettingsDbt>, ISettingsViewModel<SettingsDbt>
    {
        private Command pickImgeCommand;

        public SettingViewModel(INavigationService? ns) : base(ns)
        {
            this.Title = Resources.CreateCustomer;
        }

        public Command PickImageCommand => this.pickImgeCommand ??= new Command(async () => await this.PickImage());

        private async Task PickImage()
        {
            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } },
                    { DevicePlatform.UWP, new[] { ".cbr", ".cbz" } },
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
                });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            
            await this.PickAndShow(options);
        }
        
        async Task PickAndShow(PickOptions options)
            {
                try
                {
                    var result = await FilePicker.PickAsync();
                    if (result != null)
                    {
                        var Text = $"File Name: {result.FileName}";
                        if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                            result.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase) ||
                            result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                        {
                            var path = await DependencyService.Get<ICopyDataToLocalStorageService>().CopyDataToLocalStore(await FilePicker.PickAsync(options));

                            Model.LogoPath = path;
                        }
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
}
