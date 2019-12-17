using Billmanager.Interfaces;
using System.IO;
using Windows.Storage;
using Billmanager.Interfaces.Database;
using Billmanager.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbPath))]
namespace Billmanager.UWP.Services
{
    public class DbPath : IDbPath
    {
        public string GetDbStoragePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path);
        }
    }
}