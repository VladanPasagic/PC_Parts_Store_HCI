using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class UpdateManufacturerCommand : IUpdateManufacturerCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public UpdateManufacturerCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Manufacturer manufacturer)
    {
        using var context = _contextFactory.Create();

        var manufacturerDto = context.Manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturer.ManufacturerId);

        manufacturerDto!.Name = manufacturer.Name;
        manufacturerDto!.Country = manufacturer.Country;

        context.Manufacturers.Update(manufacturerDto);

        await context.SaveChangesAsync();

    }
}
