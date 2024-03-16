namespace PCPartsStore.Domain.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public decimal TotalPrice { get; set; }

    public int SellerId { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual Seller Seller { get; set; } = null!;
}
