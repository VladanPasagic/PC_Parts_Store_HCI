namespace PCPartsStore.Domain.Models;

public partial class Setting
{
    public int SellerId { get; set; }

    public int Language { get; set; }

    public int Theme { get; set; }

    public virtual Seller Seller { get; set; } = null!;
}
