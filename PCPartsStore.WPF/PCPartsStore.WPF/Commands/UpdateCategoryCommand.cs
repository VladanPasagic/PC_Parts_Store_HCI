using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class UpdateCategoryCommand : AsyncCommandBase
{
    private const string FieldEmpty = "FieldEmpty";
    private readonly CategoriesViewModel _categoriesViewModel;

    public UpdateCategoryCommand(CategoriesViewModel categoriesViewModel)
    {
        _categoriesViewModel = categoriesViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        var selected = _categoriesViewModel.SelectedCategory;

        var viewModel = _categoriesViewModel.CategoryDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel!.Name))
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance[FieldEmpty];
            return;
        }

        await CategoryStore.Instance!.Update(new CategoryOO()
        {
            CategoryId = selected!.CategoryId,
            Name = viewModel.Name,
        });
    }
}
