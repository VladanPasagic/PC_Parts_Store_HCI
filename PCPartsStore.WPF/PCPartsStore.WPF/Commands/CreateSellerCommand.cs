using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class CreateSellerCommand : CommandBase
{
    private const string FieldEmpty = "FieldEmpty";
    private readonly SellersViewModel _sellersViewModel;

    public CreateSellerCommand(SellersViewModel sellersViewModel)
    {
        _sellersViewModel = sellersViewModel;
    }

    public override void Execute(object? parameter)
    {
        var viewModel = _sellersViewModel.SellerRegistrationDetailsFormViewModel!;

        if (string.IsNullOrEmpty(viewModel.Password) || string.IsNullOrEmpty(viewModel.Username))
        {
            viewModel.ErrorMessage = LocalizedStrings.Instance[FieldEmpty];
            _ = Methods.WaitAndRemoveErrorMessage(viewModel);
            return;
        }

        Seller seller = new()
        {
            Username = viewModel.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            IsActive = true,
            IsAdmin = false
        };

        _ = SellerStore.Instance!.Register(seller);
    }
}
