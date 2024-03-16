using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

class LogoutMenuItemCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        NavigationStore.Instance!.CurrentViewModel = new LoginViewModel();
    }
}
