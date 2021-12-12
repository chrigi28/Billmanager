using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Billmanager.Interfaces.Data;
using Billmanager.Interfaces.Database.Datatables;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

using static Billmanager.Interfaces.Data.BillmanagerMessages;

namespace Billmanager.Database.Tables;

public class ItemPositionDbt : BaseDbt, IItemPositionDbt
{
    private decimal _price;
    private decimal _amount;
    private string _description;

    [ForeignKey(nameof(BillDbt.ItemPositions))]
    public int BillId { get; set; }
    public virtual BillDbt Bill { get; set; }

    public string Description
    {
        get => _description;
        set => this.SetProperty(ref this._description, value);
    }

    public decimal Amount
    {
        get => _amount;
        set => this.SetProperty(ref this._amount, value, onChanged: this.NotifyTotalChanged);
    }

    public decimal Price
    {
        get => _price;
        set => this.SetProperty(ref this._price, value, onChanged: this.NotifyTotalChanged);
    }

    public decimal Total => this.Price * this.Amount;

    private void NotifyTotalChanged()
    {
        this.OnPropertyChanged(nameof(this.Total));
            
    }
}