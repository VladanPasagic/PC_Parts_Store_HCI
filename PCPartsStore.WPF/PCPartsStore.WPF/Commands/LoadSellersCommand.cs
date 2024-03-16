using PCPartsStore.WPF.Stores;

namespace PCPartsStore.WPF.Commands;

public class LoadSellersCommand : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        await SellerStore.Instance!.Read();
    }
}
