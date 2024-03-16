using PCPartsStore.Domain.Commands;

namespace PCPartsStore.EntityFramework.Commands;

public class DeleteArticleCommand : IDeleteArticleCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public DeleteArticleCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(int id)
    {
        using var context = _contextFactory.Create();

        context.Articles.Remove(new DTOs.ArticleDTO()
        {
            ArticleId = id
        });

        await context.SaveChangesAsync();
    }
}
