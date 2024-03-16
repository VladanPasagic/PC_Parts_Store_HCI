namespace PCPartsStore.EntityFramework.DTOs;

public partial class ArticleHasSupplierDTO
{
    public int ArticleId { get; set; }

    public int SupplierId { get; set; }

    public int Quantity { get; set; }

    public decimal PurchasePrice { get; set; }

    public virtual ArticleDTO Article { get; set; } = null!;

    public virtual SupplierDTO Supplier { get; set; } = null!;
}
