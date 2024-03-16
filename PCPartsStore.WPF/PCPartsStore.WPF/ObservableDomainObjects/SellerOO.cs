using PCPartsStore.Domain.Models;

namespace PCPartsStore.WPF.ObservableDomainObjects;

public class SellerOO : ObservableObject
{
    private Seller _seller;

    public SellerOO()
    {
        _seller = new();
    }

    public int SellerId
    {
        get => _seller.SellerId;
        set
        {
            _seller.SellerId = value;
            OnPropertyChanged(nameof(SellerId));
        }
    }

    public string Username
    {
        get => _seller.Username;
        set
        {
            _seller.Username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string PasswordHash
    {
        get => _seller.PasswordHash;
        set
        {
            _seller.PasswordHash = value;
            OnPropertyChanged(nameof(PasswordHash));
        }
    }

    public bool IsAdmin
    {
        get => _seller.IsAdmin;
        set
        {
            _seller.IsAdmin = value;
            OnPropertyChanged(nameof(IsAdmin));
        }
    }

    public bool IsActive
    {
        get => _seller.IsActive;
        set
        {
            _seller.IsActive = value;
            OnPropertyChanged(nameof(IsActive));
        }
    }
}
