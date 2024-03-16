using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class OpenCategoryModalCommand : CommandBase
{
    private const string CategoryNoteSelected = "CategoryNotSelectedMessage";
    private readonly CategoriesViewModel _categoryViewModel;
    private readonly bool _isEditing;

    public OpenCategoryModalCommand(CategoriesViewModel viewModel, bool isEditing)
    {
        _categoryViewModel = viewModel;
        _isEditing = isEditing;
    }

    public override void Execute(object? parameter)
    {
        if (!_isEditing)
        {
            _categoryViewModel.CategoryDetailsFormViewModel = new(new AddCategoryCommand(_categoryViewModel),
                new CloseModalCommand(_categoryViewModel), new UpdateCategoryCommand(_categoryViewModel));
        }
        else if (_categoryViewModel.SelectedCategory != null)
        {
            _categoryViewModel.CategoryDetailsFormViewModel = new(new AddCategoryCommand(_categoryViewModel),
                new CloseModalCommand(_categoryViewModel), new UpdateCategoryCommand(_categoryViewModel), _categoryViewModel.SelectedCategory);
        }
        else
        {
            _categoryViewModel.ErrorMessage = LocalizedStrings.Instance[CategoryNoteSelected];
            _ = Methods.WaitAndRemoveErrorMessage(_categoryViewModel);
            return;
        }

        _categoryViewModel.IsOpen = true;
    }
}
