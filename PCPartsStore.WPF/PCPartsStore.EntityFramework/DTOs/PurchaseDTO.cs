namespace PCPartsStore.EntityFramework.DTOs;

public partial class PurchaseDTO
{
    public int ArticleId { get; set; }

    public int SupplierId { get; set; }

    public int Quantity { get; set; }

    public int PurchaseId { get; set; }

    public decimal? Price { get; set; }

    public virtual ArticleDTO Article { get; set; } = null!;

    public virtual SupplierDTO Supplier { get; set; } = null!;
}
