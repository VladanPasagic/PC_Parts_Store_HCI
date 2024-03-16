using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class AddProductCommand : AsyncCommandBase
{
    private const string RequiredFieldsMissing = "RequiredFieldsMissing";
    private readonly ProductsViewModel _productsViewModel;

    public AddProductCommand(ProductsViewModel productsViewModel)
    {
        _productsViewModel = productsViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        var viewModel = _productsViewModel.ProductDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel!.Name) || viewModel.ManufacturingYear < 1900 || viewModel.ManufacturerIndex == -1 ||
             viewModel.CategoryIndex == -1 || viewModel.Price < 0)
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance[RequiredFieldsMissing];
            _ = Methods.WaitAndRemoveErrorMessage(viewModel);
            return;
        }

        ArticleOO article = new()
        {
            Name = viewModel.Name,
            Price = viewModel.Price,
            Manufacturer = viewModel.Manufacturers[viewModel.ManufacturerIndex],
            Category = viewModel.Categories[viewModel.CategoryIndex],
            ManufacturerId = viewModel.Manufacturers[viewModel.ManufacturerIndex].ManufacturerId,
            CategoryId = viewModel.Categories[viewModel.CategoryIndex].CategoryId,
            Quantity = viewModel.Quantity,
            ManufacturingYear = viewModel.ManufacturingYear,
        };

        await ProductStore.Instance!.Create(article);
    }
}
