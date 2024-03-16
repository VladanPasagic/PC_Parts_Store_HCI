using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;

namespace PCPartsStore.EntityFramework.Queries;

public class GetSettingsQuery : IGetSettingsQuery
{
    private readonly PcStoreContextFactory _contextFactory;

    public GetSettingsQuery(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Setting? Execute(Seller seller)
    {
        using var context = _contextFactory.Create();

        var settings = context.Settings.FirstOrDefault(s => s.SellerId == seller.SellerId);

        if (settings != null)
        {
            return new Setting()
            {
                Theme = settings.Theme,
                Language = settings.Language,
                SellerId = settings.SellerId,
            };
        };

        return null;
    }
}
