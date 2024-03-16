using PCPartsStore.Domain.Models;

namespace PCPartsStore.WPF.ObservableDomainObjects;

public class ManufacturerOO : ObservableObject
{
    private Manufacturer _manufacturer;

    public ManufacturerOO()
    {
        _manufacturer = new();
    }

    public ManufacturerOO(Manufacturer manufacturer)
    {
        _manufacturer = new();
        ManufacturerId = manufacturer.ManufacturerId;
        Name = manufacturer.Name;
        Country = manufacturer.Country;
    }

    public string Name
    {
        get => _manufacturer.Name;
        set
        {
            _manufacturer.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Country
    {
        get => _manufacturer.Country;

        set
        {
            _manufacturer.Country = value;
            OnPropertyChanged(nameof(Country));
        }
    }

    public int ManufacturerId
    {
        get => _manufacturer.ManufacturerId;
        set
        {
            _manufacturer.ManufacturerId = value;
            OnPropertyChanged(nameof(ManufacturerId));
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not null and ManufacturerOO other)
        {
            return other.ManufacturerId == ManufacturerId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return ManufacturerId.GetHashCode();
    }
}
