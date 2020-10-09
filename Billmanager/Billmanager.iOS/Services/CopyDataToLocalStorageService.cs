using System.Threading.Tasks;
using Billmanager.iOS.Services;
using Foundation;
using Billmanager.Interfaces.Service;

using Xamarin.Essentials;
using Xamarin.Forms;

#nullable enable

[assembly: Dependency(typeof(CopyDataToLocalStorageService))]

// ReSharper disable once CheckNamespace
namespace Billmanager.iOS.Services
{
    /// <summary>
    /// Sound Service
    /// </summary>
    [Preserve(AllMembers = true)]
    public sealed class CopyDataToLocalStorageService : ICopyDataToLocalStorageService
    {
        /// <param name="file"></param>
        /// <inheritdoc/>
        public Task<string?> CopyDataToLocalStore(FileResult file)
        {
            // nothing to do is a uwp service
            return Task.FromResult((string?)string.Empty);
        }

        /// <inheritdoc/>
        public void DeleteTempFile(string file)
        {
            // dummy
        }
    }
}