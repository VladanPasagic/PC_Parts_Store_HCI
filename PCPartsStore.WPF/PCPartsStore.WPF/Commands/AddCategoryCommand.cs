using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class AddCategoryCommand : AsyncCommandBase
{
    private const string FieldEmpty = "FieldEmpty";
    private readonly CategoriesViewModel _categoriesViewModel;

    public AddCategoryCommand(CategoriesViewModel categoriesViewModel)
    {
        _categoriesViewModel = categoriesViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        var viewModel = _categoriesViewModel.CategoryDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel!.Name))
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance[FieldEmpty];
            _ = Methods.WaitAndRemoveErrorMessage(viewModel);
            return;
        }

        CategoryOO category = new()
        {
            Name = viewModel.Name,
        };

        await CategoryStore.Instance!.Create(category);
    }
}
