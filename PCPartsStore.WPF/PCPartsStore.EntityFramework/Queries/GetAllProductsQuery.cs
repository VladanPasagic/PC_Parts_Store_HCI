using Microsoft.EntityFrameworkCore;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework.Queries;

public class GetAllProductsQuery : IGetAllProductsQuery
{
    private readonly PcStoreContextFactory _contextFactory;

    public GetAllProductsQuery(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Article>> Execute()
    {
        using var context = _contextFactory.Create();

        IEnumerable<ArticleDTO> articleDTOs = await context.Articles.Include(a => a.Category).Include(a => a.Manufacturer).ToListAsync();

        return articleDTOs.Select(p => new Article()
        {
            ManufacturingYear = p.ManufacturingYear,
            Name = p.Name,
            Category = new Category()
            {
                CategoryId = p.CategoryId,
                Name = p.Category.Name,
            },
            Manufacturer = new Manufacturer()
            {
                ManufacturerId = p.ManufacturerId,
                Name = p.Manufacturer.Name,
                Country = p.Manufacturer.Country,
            },
            ArticleId = p.ArticleId,
            Quantity = p.Quantity,
            Price = p.Price,
            CategoryId = p.CategoryId,
            ManufacturerId = p.ManufacturerId,
        });
    }
}
