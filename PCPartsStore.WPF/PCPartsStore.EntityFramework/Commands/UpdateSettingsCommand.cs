using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class UpdateSettingsCommand : IUpdateSettingsCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public UpdateSettingsCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Setting setting)
    {
        using var context = _contextFactory.Create();

        var settings = context.Settings.FirstOrDefault(s => s.SellerId == setting.SellerId);

        settings!.Theme = setting.Theme;
        settings!.Language = setting.Language;

        context.Settings.Update(settings);

        await context.SaveChangesAsync();
    }
}
