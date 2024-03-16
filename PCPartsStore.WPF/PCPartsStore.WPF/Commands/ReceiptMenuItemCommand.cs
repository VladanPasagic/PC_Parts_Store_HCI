using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class ReceiptMenuItemCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        AdminNavigationStore.Instance!.CurrentViewModel = new ReceiptsViewModel();
    }
}
