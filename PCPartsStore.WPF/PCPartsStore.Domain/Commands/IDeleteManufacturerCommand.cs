namespace PCPartsStore.Domain.Commands;

public interface IDeleteManufacturerCommand
{
    Task ExecuteAsync(int id);
}
