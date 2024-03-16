using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class UpdateProductCommand : AsyncCommandBase
{
    private const string RequiredFieldsMissing = "RequiredFieldsMissing";
    private readonly ProductsViewModel _productsViewModel;

    public UpdateProductCommand(ProductsViewModel productsViewModel)
    {
        _productsViewModel = productsViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        var selected = _productsViewModel.SelectedArticle;

        var viewModel = _productsViewModel.ProductDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel!.Name) || viewModel.ManufacturingYear < 1900 || viewModel.ManufacturerIndex == -1 ||
             viewModel.CategoryIndex == -1 || viewModel.Price < 0)
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance[RequiredFieldsMissing];
            _ = Methods.WaitAndRemoveErrorMessage(viewModel);
            return;
        };

        await ProductStore.Instance!.Update(new ArticleOO()
        {
            ManufacturerId = viewModel.Manufacturers[viewModel.ManufacturerIndex].ManufacturerId,
            CategoryId = viewModel.Categories[viewModel.CategoryIndex].CategoryId,
            ArticleId = selected!.ArticleId,
            Name = viewModel.Name,
            Price = viewModel.Price,
            Quantity = viewModel.Quantity,
            ManufacturingYear = viewModel.ManufacturingYear,
            Manufacturer = viewModel.Manufacturers[viewModel.ManufacturerIndex],
            Category = viewModel.Categories[viewModel.CategoryIndex],
        });
    }
}
