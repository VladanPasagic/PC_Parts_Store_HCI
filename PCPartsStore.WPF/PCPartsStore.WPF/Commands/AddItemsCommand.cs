using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class AddItemsCommand : CommandBase
{
    private const string NoReceiptErrorMessage = "NoReceiptErrorMessage";
    private readonly SellerViewModel _sellerViewModel;

    public AddItemsCommand(SellerViewModel sellerViewModel)
    {
        _sellerViewModel = sellerViewModel;
    }

    public override void Execute(object? parameter)
    {
        if (_sellerViewModel.SelectedArticle != null)
        {
            _sellerViewModel.ItemQuantityFormViewModel = new ItemQuantityFormViewModel(new AddMultipleItems(_sellerViewModel),
                new CloseModalCommand(_sellerViewModel), _sellerViewModel.SelectedArticle);
            _sellerViewModel.IsOpen = true;
        }
        else
        {
            _sellerViewModel.ErrorMessage = LocalizedStrings.Instance[NoReceiptErrorMessage];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel);
        }
    }
}
