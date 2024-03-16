namespace PCPartsStore.EntityFramework.DTOs;

public partial class ArticleDTO
{
    public int ArticleId { get; set; }

    public string Name { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public int ManufacturingYear { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<ArticleHasSupplierDTO> ArticleHasSuppliers { get; set; } = new List<ArticleHasSupplierDTO>();

    public virtual CategoryDTO Category { get; set; } = null!;

    public virtual ICollection<ItemDTO> Items { get; set; } = new List<ItemDTO>();

    public virtual ManufacturerDTO Manufacturer { get; set; } = null!;

    public virtual ICollection<PurchaseDTO> Purchases { get; set; } = new List<PurchaseDTO>();
}
