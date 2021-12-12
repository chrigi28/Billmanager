using System.ComponentModel.DataAnnotations;
using System.IO;
using Billmanager.Database.Annotations;
using Billmanager.Interfaces.Database.Datatables;
using PropertyChanged;
using Xamarin.Forms;

namespace Billmanager.Database.Tables;

public class SettingsDbt : BaseDbt, ISettingsDbt
{
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Zip { get; set; }
    public string Location { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string IBAN { get; set; }
    public string LogoPath { get; set; }
    public ImageSource Image => GetImage();

    [CanBeNull]
    private ImageSource GetImage()
    {
        if (LogoPath != null)
        {
            return ImageSource.FromFile(LogoPath);
        }
            
        return null;
    }

    [AlsoNotifyFor(nameof(FirstName))]
    public override bool CanSave => !string.IsNullOrEmpty(this.FirstName);

    public override string FilterString => base.FilterString + this.Title + LastName + Address + Zip +
                                           Location + Phone + Email + base.FilterString;
}