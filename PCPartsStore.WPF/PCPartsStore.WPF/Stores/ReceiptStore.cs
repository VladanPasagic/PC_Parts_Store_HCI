using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;

namespace PCPartsStore.WPF.Stores;

public class ReceiptStore
{
    public static ReceiptStore? Instance { get; set; }

    private readonly IGetAllReceiptsQuery _getAllReceiptsQuery;
    private readonly ICreateReceiptCommand _createReceiptCommand;

    private readonly List<Receipt> _receipts = [];

    public IEnumerable<Receipt> Receipts => _receipts;

    public event Action? ReceiptsRead;
    public event Action? ReceiptCreated;

    public static void CreateInstance(IGetAllReceiptsQuery getAllReceiptsQuery, ICreateReceiptCommand createReceiptCommand)
    {
        Instance ??= new(getAllReceiptsQuery, createReceiptCommand);
    }

    private ReceiptStore(IGetAllReceiptsQuery getAllReceiptsQuery, ICreateReceiptCommand createReceiptCommand)
    {
        _getAllReceiptsQuery = getAllReceiptsQuery;
        _createReceiptCommand = createReceiptCommand;
    }

    public async Task Create(Receipt receipt)
    {
        await _createReceiptCommand.ExecuteAsync(receipt);

        _receipts.Add(receipt);

        ReceiptCreated?.Invoke();
    }

    public async Task Read()
    {
        var receipts = await _getAllReceiptsQuery.ExecuteAsync();

        _receipts.Clear();

        _receipts.AddRange(receipts);

        ReceiptsRead?.Invoke();
    }


}
