using PCPartsStore.Domain.Commands;

namespace PCPartsStore.EntityFramework.Commands;

public class DeleteManufacturerCommand : IDeleteManufacturerCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public DeleteManufacturerCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(int id)
    {
        using var context = _contextFactory.Create();

        context.Manufacturers.Remove(new DTOs.ManufacturerDTO()
        {
            ManufacturerId = id,
        });

        await context.SaveChangesAsync();
    }
}
