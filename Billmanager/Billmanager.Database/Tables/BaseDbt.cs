using System.ComponentModel.DataAnnotations;
using Billmanager.Interfaces.Database;

namespace Billmanager.Database.Tables
{
    public class BaseDbt : IDatabaseTable
    {
        [Key]
        [Required]
        public int Id { get; set; } = -1;
        public bool Deleted { get; set; }
        public string TableName { get; set; }
        public string Remark { get; set; }
        public virtual string FilterString => this.Id + this.TableName + this.Remark;
    }
}