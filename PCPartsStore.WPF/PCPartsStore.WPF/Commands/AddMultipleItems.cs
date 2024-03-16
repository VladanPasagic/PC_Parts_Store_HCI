using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class AddMultipleItems : CommandBase
{
    private const string QuantityExceeded = "QuantityExceededError";
    private const string CannotAddZero = "CannotAddZeroArticlesError";
    private readonly SellerViewModel _sellerViewModel;

    public AddMultipleItems(SellerViewModel sellerViewModel)
    {
        _sellerViewModel = sellerViewModel;
    }

    public override void Execute(object? parameter)
    {
        int quantity = _sellerViewModel.ItemQuantityFormViewModel!.Quantity;
        if (quantity == 0)
        {
            _sellerViewModel.ItemQuantityFormViewModel.ErrorMessage = LocalizedStrings.Instance[CannotAddZero];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel.ItemQuantityFormViewModel);
            return;
        }

        var article = _sellerViewModel.SelectedArticle;

        var item = _sellerViewModel.Items.FirstOrDefault(i => i.ArticleId == article!.ArticleId);

        if (quantity > article!.Quantity)
        {
            _sellerViewModel.ItemQuantityFormViewModel.ErrorMessage = LocalizedStrings.Instance[QuantityExceeded];
            _ = Methods.WaitAndRemoveErrorMessage(_sellerViewModel.ItemQuantityFormViewModel);
            return;
        }

        if (item == null)
        {
            _sellerViewModel.Items.Add(new ObservableDomainObjects.ItemOO()
            {
                Article = article!,
                ArticleId = article!.ArticleId,
                Quantity = quantity,
                Price = article.Price
            });
        }
        else
        {
            item.Quantity += quantity;
        }
        _sellerViewModel.Price += quantity * article.Price;

        article!.Quantity -= quantity;
    }
}
