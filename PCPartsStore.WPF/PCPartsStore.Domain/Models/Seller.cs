namespace PCPartsStore.Domain.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public bool IsActive { get; set; }

}
