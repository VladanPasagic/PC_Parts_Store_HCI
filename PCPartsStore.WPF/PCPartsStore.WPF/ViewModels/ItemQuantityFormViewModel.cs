using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class ItemQuantityFormViewModel : ViewModelBase, IModalContentViewModel, IErrorViewModel
{
    private string? _errorMessage;

    public ItemQuantityFormViewModel(ICommand addItemsToReceiptCommand, ICommand closeModalCommand, ArticleOO article)
    {
        Name = article.Name;
        ModalButtonControlViewModel = new(false, addItemsToReceiptCommand, closeModalCommand, null);
    }

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

    public int Quantity { get; set; }

    public string? Name { get; }

    public ModalButtonControlViewModel ModalButtonControlViewModel { get; }


}
