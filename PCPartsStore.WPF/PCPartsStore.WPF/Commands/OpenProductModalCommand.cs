using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class OpenProductModalCommand : CommandBase
{
    private readonly ProductsViewModel _productsViewModel;
    private readonly bool _isEditing;

    public OpenProductModalCommand(ProductsViewModel productsViewModel, bool isEditing)
    {
        _productsViewModel = productsViewModel;
        _isEditing = isEditing;
    }

    public override void Execute(object? parameter)
    {
        if (!_isEditing)
        {
            _productsViewModel.ProductDetailsFormViewModel = new(new AddProductCommand(_productsViewModel), new CloseModalCommand(_productsViewModel),
                new UpdateProductCommand(_productsViewModel));
        }
        else if (_productsViewModel.SelectedArticle != null)
        {
            _productsViewModel.ProductDetailsFormViewModel = new(new AddProductCommand(_productsViewModel), new CloseModalCommand(_productsViewModel),
                new UpdateProductCommand(_productsViewModel), _productsViewModel.SelectedArticle);
        }
        else
        {
            _productsViewModel.ErrorMessage = "You have to select a product";
            _ = Methods.WaitAndRemoveErrorMessage(_productsViewModel);
            return;
        }

        _productsViewModel.IsOpen = true;
    }
}


