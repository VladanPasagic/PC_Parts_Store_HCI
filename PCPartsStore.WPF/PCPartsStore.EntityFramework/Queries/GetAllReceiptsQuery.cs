using Microsoft.EntityFrameworkCore;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;

namespace PCPartsStore.EntityFramework.Queries;

public class GetAllReceiptsQuery : IGetAllReceiptsQuery
{
    private readonly PcStoreContextFactory _contextFactory;

    public GetAllReceiptsQuery(PcStoreContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Receipt>> ExecuteAsync()
    {
        using var context = _contextFactory.Create();

        var receiptsDto = await context.Receipts.Include(r => r.Seller).Include(r => r.Items).ThenInclude(i => i.Article).ToListAsync();

        return receiptsDto.Select(r => new Receipt()
        {
            ReceiptId = r.ReceiptId,
            Items = r.Items.Select(i => new Item()
            {
                ArticleId = i.ArticleId,
                ReceiptId = i.ReceiptId,
                Article = new Article()
                {
                    Name = i.Article.Name,
                    ArticleId = i.Article.ArticleId
                },
                Price = i.Price,
                Quantity = i.Quantity,
            }).ToList(),
            SellerId = r.SellerId,
            Seller = new Seller()
            {
                Username = r.Seller.Username,
                SellerId = r.Seller.SellerId,
            },
            TotalPrice = r.TotalPrice
        });

    }
}
