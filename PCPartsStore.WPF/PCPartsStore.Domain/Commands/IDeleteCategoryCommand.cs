namespace PCPartsStore.Domain.Commands;

public interface IDeleteCategoryCommand
{
    Task ExecuteAsync(int id);
}
