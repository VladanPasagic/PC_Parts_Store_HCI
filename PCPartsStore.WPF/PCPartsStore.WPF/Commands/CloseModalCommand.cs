using PCPartsStore.WPF.ViewModels.Interfaces;

namespace PCPartsStore.WPF.Commands;

public class CloseModalCommand : CommandBase
{
    private readonly IModalViewModel _viewModel;

    public CloseModalCommand(IModalViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        _viewModel.IsOpen = false;
    }
}
