using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Billmanager.Droid.Services;
using Billmanager.Interfaces;
using Billmanager.Interfaces.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbPath))]
namespace Billmanager.Droid.Services
{
    class DbPath : IDbPath
    {
        public string GetDbStoragePath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }
    }
}