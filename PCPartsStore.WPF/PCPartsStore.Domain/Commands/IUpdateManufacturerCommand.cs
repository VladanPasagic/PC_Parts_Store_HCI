using PCPartsStore.Domain.Models;

namespace PCPartsStore.Domain.Commands;

public interface IUpdateManufacturerCommand
{
    Task ExecuteAsync(Manufacturer manufacturer);
}
