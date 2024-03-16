using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class DeleteProductCommand : AsyncCommandBase
{
    private const string ProductNotSelectedMessage = "ProductNotSelectedMessage";
    private readonly ProductsViewModel _productsViewModel;

    public DeleteProductCommand(ProductsViewModel productsViewModel)
    {
        _productsViewModel = productsViewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        if (_productsViewModel.SelectedArticle == null)
        {
            _productsViewModel.ErrorMessage = LocalizedStrings.Instance[ProductNotSelectedMessage];
            _ = Methods.WaitAndRemoveErrorMessage(_productsViewModel);
            return;
        }

        await ProductStore.Instance!.Delete(_productsViewModel.SelectedArticle.ArticleId);
    }
}
