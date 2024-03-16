namespace PCPartsStore.Domain.Commands;

public interface IDeleteArticleCommand
{
    Task ExecuteAsync(int id);
}
