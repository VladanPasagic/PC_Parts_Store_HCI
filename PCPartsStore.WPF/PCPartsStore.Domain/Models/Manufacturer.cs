namespace PCPartsStore.Domain.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        if (obj is not null and Manufacturer other)
        {
            return other.Name.Equals(Name) && other.Country.Equals(Country);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
