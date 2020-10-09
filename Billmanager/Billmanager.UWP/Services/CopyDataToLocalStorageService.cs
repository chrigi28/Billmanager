using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Billmanager.Interfaces.Service;

using Windows.Storage;
using Billmanager.UWP.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

#nullable enable

[assembly: Dependency(typeof(CopyDataToLocalStorageService))]

// ReSharper disable once CheckNamespace
namespace Billmanager.UWP.Services
{
    /// <summary>
    /// Sound Service
    /// </summary>
    [Preserve(AllMembers = true)]
    public sealed class CopyDataToLocalStorageService : ICopyDataToLocalStorageService
    {
        /// <inheritdoc/>
        public async Task<string?> CopyDataToLocalStore(FileResult file)
        {
            if (file != null)
            {
                try
                {
                    var filedata = await StorageFile.GetFileFromPathAsync(file.FullPath);
                    ////var filedata = await picker.PickSingleFileAsync();
                    var path = filedata.Path;
                    var aPath = filedata.Path;
                    StorageFile? copy = null;
                    var fileNameNoEx = Path.GetFileNameWithoutExtension(aPath);
                    copy = await filedata.CopyAsync(
                        ApplicationData.Current.LocalFolder,
                        fileNameNoEx + filedata.FileType,
                        NameCollisionOption.GenerateUniqueName);

                    return copy.Path;
                }
#pragma warning disable CA1031
                catch (Exception ex)
                {
                    Trace.WriteLine(@"unable to save to app directory:" + ex);
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public void DeleteTempFile(string file)
        {
#pragma warning disable CA1307 // Specify StringComparison
                var dir = new FileInfo(file).Directory.Name;
                if (!string.IsNullOrEmpty(dir) && dir == @"LocalState")
#pragma warning restore CA1307 // Specify StringComparison
                {
                    File.Delete(file);
                }
        }
    }
}