using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;

namespace PCPartsStore.EntityFramework.Commands;

public class UpdateArticleCommand : IUpdateArticleCommand
{
    private readonly PcStoreContextFactory _contextFactory;

    public UpdateArticleCommand(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task ExecuteAsync(Article article)
    {
        using var context = _contextFactory.Create();

        var product = context.Articles.FirstOrDefault(a => a.ArticleId == article.ArticleId);
        product!.CategoryId = article.CategoryId;
        product!.ManufacturingYear = article.ManufacturingYear;
        product!.ManufacturerId = article.ManufacturerId;
        product!.Quantity = article.Quantity;
        product!.Name = article.Name;
        product!.Price = article.Price;


        context.Articles.Update(product);

        await context.SaveChangesAsync();
    }
}
