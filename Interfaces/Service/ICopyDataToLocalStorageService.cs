using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Billmanager.Interfaces.Service
{
    /// <summary>Interface for uwp </summary>
    public interface ICopyDataToLocalStorageService
    {
        /// <summary>copies data to local storage for upload.</summary>
        /// <param name="file">data</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<string?> CopyDataToLocalStore(FileResult file);

        /// <summary>Moves the tempfile added for upload to its final position or deletes it</summary>
        /// <param name="file">filepath</param>
        void DeleteTempFile(string file);
    }
}