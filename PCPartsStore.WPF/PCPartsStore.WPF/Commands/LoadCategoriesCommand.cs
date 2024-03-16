using PCPartsStore.WPF.Stores;

namespace PCPartsStore.WPF.Commands;

public class LoadCategoriesCommand : AsyncCommandBase
{

    public override async Task ExecuteAsync(object? parameter)
    {
        await CategoryStore.Instance!.Read();
    }
}
