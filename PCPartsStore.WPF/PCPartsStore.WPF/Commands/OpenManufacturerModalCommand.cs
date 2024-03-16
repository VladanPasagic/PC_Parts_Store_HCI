using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class OpenManufacturerModalCommand : CommandBase
{
    private const string ManufacturerNotSelected = "ManufacturerNotSelectedMessage";
    private readonly ManufacturersViewModel _manufacturersViewModel;
    private readonly bool _isEditing;

    public OpenManufacturerModalCommand(ManufacturersViewModel manufacturersViewModel, bool isEditing)
    {
        _manufacturersViewModel = manufacturersViewModel;
        _isEditing = isEditing;
    }

    public override void Execute(object? parameter)
    {
        if (!_isEditing)
        {
            _manufacturersViewModel.ManufacturerDetailsFormViewModel = new(new AddManufacturerCommand(_manufacturersViewModel),
                new CloseModalCommand(_manufacturersViewModel), new UpdateManufacturerCommand(_manufacturersViewModel));
        }
        else if (_manufacturersViewModel.SelectedManufacturer != null)
        {
            _manufacturersViewModel.ManufacturerDetailsFormViewModel = new(new AddManufacturerCommand(_manufacturersViewModel),
                new CloseModalCommand(_manufacturersViewModel), new UpdateManufacturerCommand(_manufacturersViewModel), _manufacturersViewModel.SelectedManufacturer);
        }
        else
        {
            _manufacturersViewModel.ErrorMessage = LocalizedStrings.Instance[ManufacturerNotSelected];
            _ = Methods.WaitAndRemoveErrorMessage(_manufacturersViewModel);
            return;
        }

        _manufacturersViewModel.IsOpen = true;
    }
}
