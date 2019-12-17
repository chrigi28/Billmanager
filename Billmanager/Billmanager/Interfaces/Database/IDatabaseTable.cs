namespace Billmanager.Interfaces.Database
{
    public interface IDatabaseTable
    {
        // Database Primary key
        string Id { get; set; }
        string TableName { get; }
        string Remark { get; set; }
    }
}