namespace PCPartsStore.EntityFramework.DTOs;

public partial class ItemDTO
{
    public int Quantity { get; set; }

    public int ReceiptId { get; set; }

    public int ArticleId { get; set; }

    public decimal Price { get; set; }

    public virtual ArticleDTO Article { get; set; } = null!;

    public virtual ReceiptDTO Receipt { get; set; } = null!;
}
