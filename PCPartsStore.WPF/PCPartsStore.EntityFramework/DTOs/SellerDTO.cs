namespace PCPartsStore.EntityFramework.DTOs;

public partial class SellerDTO
{
    public int SellerId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ReceiptDTO> Receipts { get; set; } = new List<ReceiptDTO>();

    public virtual SettingDTO? Setting { get; set; }
}
