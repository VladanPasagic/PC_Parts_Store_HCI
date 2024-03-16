using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class OpenSellerModalCommand : CommandBase
{
    private readonly SellersViewModel _sellersViewModel;

    public OpenSellerModalCommand(SellersViewModel sellersViewModel)
    {
        _sellersViewModel = sellersViewModel;
    }

    public override void Execute(object? parameter)
    {
        _sellersViewModel.SellerRegistrationDetailsFormViewModel = new(new CreateSellerCommand(_sellersViewModel), new CloseModalCommand(_sellersViewModel));
        _sellersViewModel.IsOpen = true;
    }
}
