using PCPartsStore.WPF.Commands;
using PCPartsStore.WPF.ViewModels.Interfaces;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class LoginViewModel : ViewModelBase, IErrorViewModel
{
    private bool _isSubmitting;

    public bool IsSubmitting
    {
        get => _isSubmitting;
        set
        {
            _isSubmitting = value;
            OnPropertyChanged(nameof(IsSubmitting));
        }
    }
    public LoginViewModel()
    {
        LoginDetailsFormViewModel = new();
        LoginCommand = new LoginCommand(this);
    }

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


    public LoginDetailsFormViewModel LoginDetailsFormViewModel { get; }

    public ICommand LoginCommand { get; }

}
