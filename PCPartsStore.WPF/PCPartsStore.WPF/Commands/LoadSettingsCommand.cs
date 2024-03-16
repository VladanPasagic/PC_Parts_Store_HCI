using PCPartsStore.WPF.Stores;

namespace PCPartsStore.WPF.Commands;

public class LoadSettingsCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        SettingsStore.Instance!.Read();
    }
}
