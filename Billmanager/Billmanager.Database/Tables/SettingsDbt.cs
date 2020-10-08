using System.ComponentModel.DataAnnotations;
using System.IO;
using Billmanager.Interfaces.Database.Datatables;
using PropertyChanged;
using Xamarin.Forms;

namespace Billmanager.Database.Tables
{
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
        public byte[] Logo { get; set; }
        public ImageSource Image 
        {
            get
            {
                return this.GetImage();
            }
        }

        private Image GetImage()
        {
            using (MemoryStream ms = new MemoryStream(Logo))
            {
                return Image.FromStream(ms);
            }
        }

        [AlsoNotifyFor(nameof(FirstName))]
        public override bool CanSave => !string.IsNullOrEmpty(this.FirstName);

        public override string FilterString => base.FilterString + this.Title + LastName + Address + Zip +
                                               Location + Phone + Email + base.FilterString;
    }
}