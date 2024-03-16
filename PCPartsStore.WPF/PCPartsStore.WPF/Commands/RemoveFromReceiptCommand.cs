using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class RemoveFromReceiptCommand : CommandBase
{
    private const string ItemNotSelected = "ItemNotSelected";
    private readonly SellerViewModel _sellerViewModel;

    public RemoveFromReceiptCommand(SellerViewModel sellerViewModel)
    {
        _sellerViewModel = sellerViewModel;
    }

    public override void Execute(object? parameter)
    {
        var item = _sellerViewModel.SelectedItem;

        if (item == null)
        {
            _sellerViewModel.ErrorMessage = LocalizedStrings.Instance[ItemNotSelected];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel);
            return;
        }
        _sellerViewModel.Items.Remove(item!);

        var product = _sellerViewModel.Products.FirstOrDefault(a => a.ArticleId == item.ArticleId);
        product!.Quantity += item.Quantity;
        _sellerViewModel.Price -= item.Price * item.Quantity;
    }

}
