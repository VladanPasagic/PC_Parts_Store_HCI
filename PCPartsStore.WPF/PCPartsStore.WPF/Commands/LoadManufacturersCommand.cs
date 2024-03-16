using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels.Interfaces;

namespace PCPartsStore.WPF.Commands;

public class LoadManufacturersCommand : AsyncCommandBase
{

    public override async Task ExecuteAsync(object? parameter)
    {
        await ManufacturerStore.Instance!.Read();
    }
}
