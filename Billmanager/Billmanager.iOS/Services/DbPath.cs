using System.IO;
using Billmanager.Interfaces.Database;
using Billmanager.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbPath))]
namespace Billmanager.iOS.Services
{
    class DbPath : IDbPath
    {
        public string GetDbStoragePath()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "..", "Library", "data");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            return path;
        }
    }
}