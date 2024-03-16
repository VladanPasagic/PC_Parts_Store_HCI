namespace PCPartsStore.Domain.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        if (obj is not null and Category other)
        {
            return other.Name.Equals(Name);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
