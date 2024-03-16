namespace PCPartsStore.Domain.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string Name { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public int ManufacturingYear { get; set; }

    public int Quantity { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
