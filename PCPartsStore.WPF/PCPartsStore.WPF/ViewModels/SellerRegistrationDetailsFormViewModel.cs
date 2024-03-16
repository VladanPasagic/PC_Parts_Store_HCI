using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class SellerRegistrationDetailsFormViewModel : ViewModelBase, IModalContentViewModel, IErrorViewModel
{
    public SellerRegistrationDetailsFormViewModel(ICommand createSellerCommand, ICommand closeModalCommand)
    {
        ModalButtonControlViewModel = new(false, createSellerCommand, closeModalCommand, null);
    }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public ModalButtonControlViewModel ModalButtonControlViewModel { get; }

    private string? _errorMessage;

    public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

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
}
