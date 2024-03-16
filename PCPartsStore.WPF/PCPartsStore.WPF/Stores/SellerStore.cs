using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;

namespace PCPartsStore.WPF.Stores;

public class SellerStore
{
    public static SellerStore? Instance { get; private set; }

    private readonly IGetAllSellersQuery _getAllSellersQuery;
    private readonly ILoginSellerCommand _loginSellerCommand;
    private readonly IRegisterSellerCommand _registerSellerCommand;
    private readonly IDeactivateSellerCommand _deactivateSellerCommand;

    public event Action<Seller>? SellerCreated;
    public event Action? SellersRead;
    public event Action<Seller>? SellerDeactivated;


    private readonly List<Seller> _sellers = [];
    public IEnumerable<Seller> Sellers => _sellers;

    public static void CreateInstance(ILoginSellerCommand loginSellerCommand, IRegisterSellerCommand registerSellerCommand,
        IGetAllSellersQuery getAllSellersQuery, IDeactivateSellerCommand deactivateSellerCommand)
    {
        Instance ??= new(loginSellerCommand, registerSellerCommand, getAllSellersQuery, deactivateSellerCommand);
    }

    private SellerStore(ILoginSellerCommand loginSellerCommand, IRegisterSellerCommand registerSellerCommand,
        IGetAllSellersQuery getAllSellersQuery, IDeactivateSellerCommand deactivateSellerCommand)
    {
        _loginSellerCommand = loginSellerCommand;
        _registerSellerCommand = registerSellerCommand;
        _getAllSellersQuery = getAllSellersQuery;
        _deactivateSellerCommand = deactivateSellerCommand;
    }

    private Seller? _activeSeller;

    public Seller? ActiveSeller
    {
        get => _activeSeller;

        set => _activeSeller = value;
    }

    public async Task Deactivate(Seller seller)
    {
        await _deactivateSellerCommand.ExecuteAsync(seller);
        int currentIndex = _sellers.FindIndex(s => s.SellerId == seller.SellerId);

        if (currentIndex != -1)
        {
            _sellers[currentIndex] = seller;
        }

        SellerDeactivated?.Invoke(seller);
    }

    public async Task Read()
    {
        IEnumerable<Seller> sellers = await _getAllSellersQuery.Execute();

        _sellers.Clear();

        _sellers.AddRange(sellers);

        SellersRead?.Invoke();
    }

    public async Task<(bool, string)> Register(Seller seller)
    {
        var info = await _registerSellerCommand.ExecuteAsync(seller);

        if (info.Item1)
        {
            _sellers.Add(seller);
        }

        SellerCreated?.Invoke(seller);

        return info;
    }

    public (bool success, string errorMsg, Seller? seller) Login(Seller seller)
    {
        return _loginSellerCommand.Execute(seller);
    }
}
