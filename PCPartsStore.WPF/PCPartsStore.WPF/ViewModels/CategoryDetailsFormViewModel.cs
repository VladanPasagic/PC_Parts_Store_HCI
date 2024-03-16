using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class CategoryDetailsFormViewModel : ViewModelBase, IErrorViewModel, IModalContentViewModel
{
    public CategoryDetailsFormViewModel(ICommand addCategoryCommand, ICommand closeModalCommand, ICommand editCategoryCommand, CategoryOO? category = default)
    {
        if (category != default)
        {
            ModalButtonControlViewModel = new(true, addCategoryCommand, closeModalCommand, editCategoryCommand);
            Name = category.Name;
        }
        else
        {
            ModalButtonControlViewModel = new(false, addCategoryCommand, closeModalCommand, editCategoryCommand);
        }
    }

    public string? Name { get; set; }

    public ModalButtonControlViewModel ModalButtonControlViewModel { get; }

    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);
}
