namespace Billmanager.Interfaces.Data;

public interface IItemPosition
{

    string Description { get; set; }
    int Amount { get; set; }
    decimal PricePerPiece { get; set; }
    decimal Total { get; }
}