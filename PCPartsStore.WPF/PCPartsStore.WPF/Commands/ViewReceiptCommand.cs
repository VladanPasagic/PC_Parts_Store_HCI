using PCPartsStore.WPF.Resources;
using PCPartsStore.WPF.Utilities;
using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Commands;

public class ViewReceiptCommand : CommandBase
{
    private const string ReceiptNotSelected = "ReceiptNotSelected";
    private readonly ReceiptsViewModel _viewModel;

    public ViewReceiptCommand(ReceiptsViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        if (_viewModel.SelectedReceipt == null)
        {
            _viewModel.ErrorMessage = LocalizedStrings.Instance[ReceiptNotSelected];
            _ = Methods.WaitAndRemoveErrorMessage(_viewModel);
            return;
        }

        _viewModel.ReceiptItemsViewModel = new(new CloseModalCommand(_viewModel), _viewModel.SelectedReceipt);
        _viewModel.IsOpen = true;
    }
}
