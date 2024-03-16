using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class DeleteCategoryCommand : AsyncCommandBase
{
    private const string CategoryNotSelectedMessage = "CategoryNotSelectedMessage";
    private readonly CategoriesViewModel _categoriesViewModel;

    public DeleteCategoryCommand(CategoriesViewModel categoriesViewModel)
    {
        _categoriesViewModel = categoriesViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        if (_categoriesViewModel.SelectedCategory == null)
        {
            _categoriesViewModel.ErrorMessage = LocalizedStrings.Instance[CategoryNotSelectedMessage];
            _ = Methods.WaitAndRemoveErrorMessage(_categoriesViewModel);
            return;
        }

        await CategoryStore.Instance!.Delete(_categoriesViewModel.SelectedCategory.CategoryId);
    }
}
