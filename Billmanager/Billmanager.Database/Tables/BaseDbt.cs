using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Billmanager.Database.Annotations;
using Billmanager.Interfaces.Database;
using PropertyChanged;

namespace Billmanager.Database.Tables
{
    [AddINotifyPropertyChangedInterface]
    public class BaseDbt : IDatabaseTable
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Remark { get; set; } = string.Empty;
        public virtual string FilterString => this.Id +  this.Remark;
    }
}