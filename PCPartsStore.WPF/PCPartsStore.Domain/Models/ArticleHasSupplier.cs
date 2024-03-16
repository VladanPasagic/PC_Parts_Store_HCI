namespace PCPartsStore.Domain.Models;

public partial class ArticleHasSupplier
{
    public int ArticleId { get; set; }

    public int SupplierId { get; set; }

    public int Quantity { get; set; }

    public decimal PurchasePrice { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
