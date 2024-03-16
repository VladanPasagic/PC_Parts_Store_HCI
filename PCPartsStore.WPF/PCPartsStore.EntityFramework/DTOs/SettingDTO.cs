namespace PCPartsStore.EntityFramework.DTOs;

public partial class SettingDTO
{
    public int SellerId { get; set; }

    public int Language { get; set; }

    public int Theme { get; set; }

    public virtual SellerDTO Seller { get; set; } = null!;
}
