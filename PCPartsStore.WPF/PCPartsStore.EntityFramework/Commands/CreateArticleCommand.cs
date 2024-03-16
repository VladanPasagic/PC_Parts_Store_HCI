using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework.Commands;

public class CreateArticleCommand : ICreateArticleCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public CreateArticleCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Article article)
    {
        using var context = _contextFactory.Create();
        context.Articles.Add(new ArticleDTO()
        {
            Name = article.Name,
            Quantity = article.Quantity,
            Price = article.Price,
            ManufacturerId = article.ManufacturerId,
            CategoryId = article.CategoryId,
            ManufacturingYear = article.ManufacturingYear,
        });

        await context.SaveChangesAsync();
    }
}
