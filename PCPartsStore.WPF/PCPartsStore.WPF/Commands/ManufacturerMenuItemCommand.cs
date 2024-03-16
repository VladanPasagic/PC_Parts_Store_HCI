using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

internal class ManufacturerMenuItemCommand : CommandBase
{

    public override void Execute(object? parameter)
    {
        AdminNavigationStore.Instance.CurrentViewModel = new ManufacturersViewModel();
    }
}
