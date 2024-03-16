namespace PCPartsStore.EntityFramework.DTOs;

public partial class SupplierDTO
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<ArticleHasSupplierDTO> ArticleHasSuppliers { get; set; } = new List<ArticleHasSupplierDTO>();

    public virtual ICollection<PurchaseDTO> Purchases { get; set; } = new List<PurchaseDTO>();
}
