
namespace Billmanager.Interfaces.Database
{
    public interface IDatabaseTable : IFilterStringProperty
    {
        // Database Primary key
        int Id { get; set; }
        bool Deleted { get; set; }
        string TableName { get; }
        string Remark { get; set; }
    }
}