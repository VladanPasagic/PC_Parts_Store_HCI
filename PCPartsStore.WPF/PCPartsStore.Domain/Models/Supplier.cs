namespace PCPartsStore.Domain.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<ArticleHasSupplier> ArticleHasSuppliers { get; set; } = new List<ArticleHasSupplier>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
