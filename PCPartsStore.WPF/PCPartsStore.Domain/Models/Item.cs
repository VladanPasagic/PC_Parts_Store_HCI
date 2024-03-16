namespace PCPartsStore.Domain.Models;

public partial class Item
{
    public int Quantity { get; set; }

    public int ReceiptId { get; set; }

    public int ArticleId { get; set; }

    public decimal Price { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual Receipt Receipt { get; set; } = null!;
}
