namespace PCPartsStore.Domain.Models;

public partial class Purchase
{
    public int ArticleId { get; set; }

    public int SupplierId { get; set; }

    public int Quantity { get; set; }

    public int PurchaseId { get; set; }

    public decimal? Price { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
