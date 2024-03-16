namespace PCPartsStore.EntityFramework.DTOs;

public partial class ReceiptDTO
{
    public int ReceiptId { get; set; }

    public decimal TotalPrice { get; set; }

    public int SellerId { get; set; }

    public virtual ICollection<ItemDTO> Items { get; set; } = new List<ItemDTO>();

    public virtual SellerDTO Seller { get; set; } = null!;
}
