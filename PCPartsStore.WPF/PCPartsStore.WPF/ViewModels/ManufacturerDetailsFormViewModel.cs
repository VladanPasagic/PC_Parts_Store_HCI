using PCPartsStore.WPF.ObservableDomainObjects;
using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels.Interfaces;

public class ManufacturerDetailsFormViewModel : ViewModelBase, IErrorViewModel, IModalContentViewModel
{
    public ModalButtonControlViewModel ModalButtonControlViewModel { get; }

    public ManufacturerDetailsFormViewModel(ICommand addManufacturerCommand, ICommand closeModalCommand, ICommand editManufacturerCommand, ManufacturerOO? manufacturer = default)
    {
        if (manufacturer != default)
        {
            ModalButtonControlViewModel = new(true, addManufacturerCommand, closeModalCommand, editManufacturerCommand);
            Name = manufacturer.Name;
            Country = manufacturer.Country;
        }
        else
        {
            ModalButtonControlViewModel = new(false, addManufacturerCommand, closeModalCommand, editManufacturerCommand);
        }
    }

    public string? Name { get; set; }

    public string? Country { get; set; }

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
