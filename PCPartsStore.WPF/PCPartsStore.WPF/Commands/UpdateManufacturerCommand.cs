using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class UpdateManufacturerCommand : AsyncCommandBase
{
    private const string RequiredFieldsMissing = "RequiredFieldsMissing";
    private readonly ManufacturersViewModel _manufacturersViewModel;

    public UpdateManufacturerCommand(ManufacturersViewModel manufacturersViewModel)
    {
        _manufacturersViewModel = manufacturersViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        var selected = _manufacturersViewModel.SelectedManufacturer;

        var viewModel = _manufacturersViewModel.ManufacturerDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel!.Name) || string.IsNullOrEmpty(viewModel!.Country))
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance[RequiredFieldsMissing];
            _ = Methods.WaitAndRemoveErrorMessage(viewModel);
            return;
        }

        await ManufacturerStore.Instance!.Update(new ManufacturerOO()
        {
            ManufacturerId = selected!.ManufacturerId,
            Name = viewModel.Name,
            Country = viewModel.Country,
        });
    }
}
