using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;

namespace PCPartsStore.WPF.Stores;

public class SettingsStore
{
    public static SettingsStore? Instance { get; private set; }

    private readonly IGetSettingsQuery _getSettingsQuery;
    private readonly IUpdateSettingsCommand _updateSettingsCommand;

    public Setting? CurrentSetting { get; set; }

    public static void CreateInstance(IGetSettingsQuery getSettingsQuery, IUpdateSettingsCommand updateSettingsCommand)
    {
        Instance ??= new(getSettingsQuery, updateSettingsCommand);
    }

    private SettingsStore(IGetSettingsQuery getAllSettingsQuery, IUpdateSettingsCommand updateSettingsCommand)
    {
        _getSettingsQuery = getAllSettingsQuery;
        _updateSettingsCommand = updateSettingsCommand;
    }

    public event Action<Setting>? SettingsRead;

    public void Read()
    {
        var settings = _getSettingsQuery.Execute(SellerStore.Instance!.ActiveSeller!);

        CurrentSetting = settings!;

        SettingsRead?.Invoke(settings!);
    }

    public void Update(Setting settings)
    {
        _updateSettingsCommand.ExecuteAsync(settings);
    }
}
