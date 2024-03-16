using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class AddManufacturerCommand : AsyncCommandBase
{
    private readonly ManufacturersViewModel _manufacturerViewModel;

    public AddManufacturerCommand(ManufacturersViewModel manufacturerViewModel)
    {
        _manufacturerViewModel = manufacturerViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        var viewModel = _manufacturerViewModel.ManufacturerDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel!.Name) || string.IsNullOrEmpty(viewModel!.Country))
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance["FieldEmpty"];
            return;
        }

        ManufacturerOO manufacturer = new()
        {
            Name = viewModel.Name,
            Country = viewModel.Country,
        };

        await ManufacturerStore.Instance!.Create(manufacturer);
    }
}
