using PCPartsStore.Domain.Models;
using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Stores;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class LoginCommand : CommandBase
{
    private const string UsernameEmptyError = "UsernameEmptyErrorMessage";
    private const string PasswordEmptyError = "PasswordEmptyErrorMessage";
    private readonly LoginViewModel _viewModel;

    public LoginCommand(LoginViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        LoginDetailsFormViewModel viewModel = _viewModel.LoginDetailsFormViewModel;

        if (string.IsNullOrEmpty(viewModel.Username))
        {
            _viewModel.ErrorMessage = LocalizedStrings.Instance[UsernameEmptyError];
            _ = Methods.WaitAndRemoveErrorMessage(_viewModel);
            return;
        }
        if (string.IsNullOrEmpty(viewModel.Password))
        {
            _viewModel.ErrorMessage = LocalizedStrings.Instance[PasswordEmptyError];
            _ = Methods.WaitAndRemoveErrorMessage(_viewModel);
            return;
        }
        Seller seller = new()
        {
            Username = viewModel.Username,
            PasswordHash = viewModel.Password.Trim()
        };

        var info = SellerStore.Instance!.Login(seller);
        if (info.success == false)
        {
            _viewModel.ErrorMessage = LocalizedStrings.Instance[info.errorMsg];
            _ = Methods.WaitAndRemoveErrorMessage(_viewModel);
            return;
        }
        else
        {
            SellerStore.Instance.ActiveSeller = info.seller!;
            if (info.seller!.IsAdmin)
            {
                NavigationStore.Instance.CurrentViewModel = new AdminViewModel();
            }
            else
            {
                NavigationStore.Instance.CurrentViewModel = new SellerViewModel();
            }
        }

    }
}
