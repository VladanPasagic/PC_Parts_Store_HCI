using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class DeactivateSellerCommand : AsyncCommandBase
{
    private const string DeactivateAccountErrorMessage = "DeactivateAccountErrorMessage";
    private readonly SellersViewModel _sellersViewModel;

    public DeactivateSellerCommand(SellersViewModel sellersViewModel)
    {
        _sellersViewModel = sellersViewModel;
    }

    public async override Task ExecuteAsync(object? parameter)
    {
        Seller? selected = _sellersViewModel.SelectedSeller;

        if (selected == null)
        {
            _sellersViewModel.ErrorMessage = LocalizedStrings.Instance[DeactivateAccountErrorMessage];
            _ = Methods.WaitAndRemoveErrorMessage(_sellersViewModel);
            return;
        }
        selected.IsActive = false;
        await SellerStore.Instance!.Deactivate(selected);
    }
}
