using PCPartsStore.WPF.Stores;

namespace PCPartsStore.WPF.Commands;

public class LoadReceiptsCommand : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        await ReceiptStore.Instance!.Read();
    }
}
