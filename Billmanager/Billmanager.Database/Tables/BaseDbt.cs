using System.ComponentModel.DataAnnotations;
using Billmanager.Interfaces.Database;

namespace Billmanager.Database.Tables
{
    public class BaseDbt : IDatabaseTable
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Remark { get; set; } = string.Empty;
        public virtual string FilterString => this.Id +  this.Remark;
    }
}