using Billmanager.Interfaces.Database;

namespace Billmanager.Database.Tables
{
    public class BaseDbt : IDatabaseTable
    {
        public string Id { get; set; }
        public string TableName { get; set; }
        public string Remark { get; set; }
        public virtual string FilterString => this.Id + this.TableName + this.Remark;
    }
}