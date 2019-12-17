namespace Billmanager.Interfaces.Database.Datatables
{
    public interface IItemDbt : IDatabaseTable
    {
        string Text { get; set; }
        string Description { get; set; }
    }
}