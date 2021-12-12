using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.Runtime;
using Billmanager.Droid.Services;
using Billmanager.Interfaces.Service;

using Xamarin.Essentials;
using Xamarin.Forms;
using File = System.IO.File;

#nullable enable

[assembly: Dependency(typeof(CopyDataToLocalStorageService))]

// ReSharper disable once CheckNamespace
namespace Billmanager.Droid.Services;

/// <summary>
/// Sound Service
/// </summary>
[Preserve(AllMembers = true)]
public sealed class CopyDataToLocalStorageService : ICopyDataToLocalStorageService
{
    /// <param name="file"></param>
    /// <inheritdoc/>
    public async Task<string?> CopyDataToLocalStore(FileResult file)
    {
        var thmbfolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        var thmblibrary = Path.Combine(thmbfolder, @"storedFiles");
        if (!Directory.Exists(thmblibrary))
        {
            Directory.CreateDirectory(thmblibrary);
        }

        var thmbpath = Path.Combine(thmblibrary, file.FileName);
        try
        {
            if (File.Exists(thmbpath))
            {
                File.Delete(thmbpath);
            }
                
            using (var stream = File.OpenWrite(thmbpath))
            {
                using (var readStream = await file.OpenReadAsync())
                {
                    await readStream.CopyToAsync(stream).ConfigureAwait(false);
                }

                await stream.FlushAsync().ConfigureAwait(false);
            }

            ////File.WriteAllBytes(thmbpath, file.DataArray);
        }
#pragma warning disable CA1031
        catch (Exception ex)
        {
            Trace.WriteLine(@"unable to save to app directory:" + ex);
        }

        return thmbpath;
    }
        
    /// <inheritdoc/>
    public void DeleteTempFile(string file)
    {
#pragma warning disable CA1307 // Specify StringComparison
        var dir = new FileInfo(file).Directory.Name;
        if (!string.IsNullOrEmpty(dir) && dir == @"xamosTempFiles")
#pragma warning restore CA1307 // Specify StringComparison
        {
            File.Delete(file);
        }
    }
}