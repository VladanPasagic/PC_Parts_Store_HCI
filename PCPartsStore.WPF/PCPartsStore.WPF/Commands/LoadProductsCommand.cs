using PCPartsStore.WPF.Stores;

namespace PCPartsStore.WPF.Commands;

class LoadProductsCommand : AsyncCommandBase
{

    public override async Task ExecuteAsync(object? parameter)
    {
        await ProductStore.Instance!.Read();
    }
}
