
namespace Billmanager.Interfaces.Database;

public interface IDatabaseTable : IFilterStringProperty
{
    // Database Primary key
    int Id { get; set; }
    bool Deleted { get; set; }
    string Remark { get; set; }
    bool CanSave { get; }
}