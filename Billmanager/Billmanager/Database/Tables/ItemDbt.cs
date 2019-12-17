using Billmanager.Interfaces.Database.Datatables;

namespace Billmanager.Database.Tables
{
    public class ItemDbt : IItemDbt
    {
        public string Id { get; set; }
        public string Name => "Item";
        public string Text { get; set; }
        public string Description { get; set; }
    }
}