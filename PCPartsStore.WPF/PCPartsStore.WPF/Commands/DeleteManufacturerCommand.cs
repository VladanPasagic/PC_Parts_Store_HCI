using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class DeleteManufacturerCommand : AsyncCommandBase
{
    private const string ManufacturerNotSelected = "ManufacturerNotSelectedMessage";
    private readonly ManufacturersViewModel _manufacturersViewModel;

    public DeleteManufacturerCommand(ManufacturersViewModel manufacturersViewModel)
    {
        _manufacturersViewModel = manufacturersViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        if (_manufacturersViewModel.SelectedManufacturer == null)
        {
            _manufacturersViewModel.ErrorMessage = LocalizedStrings.Instance[ManufacturerNotSelected];
            _ = Methods.WaitAndRemoveErrorMessage(_manufacturersViewModel);
            return;
        }

        await ManufacturerStore.Instance!.Delete(_manufacturersViewModel.SelectedManufacturer.ManufacturerId);
    }
}
