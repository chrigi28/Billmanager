using Billmanager.Database.Tables;
using Xamarin.Forms;

namespace Billmanager.Helper;

public class SelectionTemplateSelector : DataTemplateSelector
{
    public DataTemplate CustomerTemplate { get; set; }
    public DataTemplate CarTemplate { get; set; }

    public DataTemplate BillTemplate { get; set; }
    ////public DataTemplate Template { get; set; }
    ////public DataTemplate CarTemplate { get; set; }

    public SelectionTemplateSelector(){}

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        switch (item)
        {
            case CustomerDbt _:
                return this.CustomerTemplate;
            case CarDbt _:
                return this.CarTemplate;
        }

        return this.CustomerTemplate;
    }
}