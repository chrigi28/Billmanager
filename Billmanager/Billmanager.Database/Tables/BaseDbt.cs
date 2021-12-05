using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Billmanager.Database.Annotations;
using Billmanager.Interfaces.Database;
using MvvmHelpers;
using PropertyChanged;

namespace Billmanager.Database.Tables
{
    [AddINotifyPropertyChangedInterface]
    public class BaseDbt : ObservableObject, IDatabaseTable, INotifyPropertyChanged 
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Remark { get; set; } = string.Empty;
        public virtual bool CanSave { get; } = true;
        public virtual string FilterString => this.Id +  this.Remark;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}