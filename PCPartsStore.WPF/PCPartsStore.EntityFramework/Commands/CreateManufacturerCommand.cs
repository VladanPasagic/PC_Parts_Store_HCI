using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class CreateManufacturerCommand : ICreateManufacturerCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public CreateManufacturerCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> ExecuteAsync(Manufacturer manufacturer)
    {
        using var context = _contextFactory.Create();

        context.Manufacturers.Add(new DTOs.ManufacturerDTO()
        {
            Name = manufacturer.Name,
            Country = manufacturer.Country,
        });

        int entries = await context.SaveChangesAsync();
        return entries > 0;
    }
}
