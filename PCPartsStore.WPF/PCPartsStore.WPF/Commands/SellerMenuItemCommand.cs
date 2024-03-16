using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

class SellerMenuItemCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        AdminNavigationStore.Instance.CurrentViewModel = new SellersViewModel();
    }
}
