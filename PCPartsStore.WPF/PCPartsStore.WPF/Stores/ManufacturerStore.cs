using PCPartsStore.Domain.Commands;
using PCPartsStore.Domain.Models;
using PCPartsStore.Domain.Queries;
using PCPartsStore.WPF.ObservableDomainObjects;
using PCPartsStore.WPF.Utilities;

namespace PCPartsStore.WPF.Stores;

public class ManufacturerStore
{
    public static ManufacturerStore? Instance { get; private set; }

    private readonly IGetAllManufacturersQuery _getAllManufacturersQuery;
    private readonly IDeleteManufacturerCommand _deleteManufacturerCommand;
    private readonly ICreateManufacturerCommand _createManufacturerCommand;
    private readonly IUpdateManufacturerCommand _updateManufacturerCommand;


    private readonly List<ManufacturerOO> _manufacturers = [];

    private ManufacturerStore(IGetAllManufacturersQuery getAllManufacturersQuery, ICreateManufacturerCommand createManufacturerCommand,
        IUpdateManufacturerCommand updateManufacturerCommand, IDeleteManufacturerCommand deleteManufacturerCommand)
    {
        _getAllManufacturersQuery = getAllManufacturersQuery;
        _createManufacturerCommand = createManufacturerCommand;
        _updateManufacturerCommand = updateManufacturerCommand;
        _deleteManufacturerCommand = deleteManufacturerCommand;
    }

    public IEnumerable<ManufacturerOO> Manufacturers => _manufacturers;

    public event Action? ManufacturerRead;
    public event Action<ManufacturerOO>? ManufacturerCreated;
    public event Action<ManufacturerOO>? ManufacturerUpdated;
    public event Action<int>? ManufacturerDeleted;

    public static void CreateInstance(IGetAllManufacturersQuery getAllManufacturersQuery, ICreateManufacturerCommand createManufacturerCommand,
        IUpdateManufacturerCommand updateManufacturerCommand, IDeleteManufacturerCommand deleteManufacturerCommand)
    {
        Instance ??= new(getAllManufacturersQuery, createManufacturerCommand, updateManufacturerCommand, deleteManufacturerCommand);
    }

    public async Task Read()
    {
        IEnumerable<Manufacturer> manufacturers = await _getAllManufacturersQuery.Execute();

        _manufacturers.Clear();

        foreach (var manufacturer in manufacturers)
        {
            _manufacturers.Add(new ManufacturerOO()
            {
                Country = manufacturer.Country,
                Name = manufacturer.Name,
                ManufacturerId = manufacturer.ManufacturerId,
            });
        }

        ManufacturerRead?.Invoke();
    }

    public async Task Delete(int id)
    {
        await _deleteManufacturerCommand.ExecuteAsync(id);

        int currentIndex = _manufacturers.FindIndex(m => m.ManufacturerId == id);

        if (currentIndex == -1)
        {
            return;
        }

        _manufacturers.RemoveAt(currentIndex);

        ManufacturerDeleted?.Invoke(id);
    }

    public async Task Create(ManufacturerOO manufacturer)
    {
        await _createManufacturerCommand.ExecuteAsync(Converters.ConvertToManufacturer(manufacturer));

        _manufacturers.Add(manufacturer);

        ManufacturerCreated?.Invoke(manufacturer);
    }

    public async Task Update(ManufacturerOO manufacturer)
    {
        await _updateManufacturerCommand.ExecuteAsync(Converters.ConvertToManufacturer(manufacturer));

        int currentIndex = _manufacturers.FindIndex(m => m.ManufacturerId == manufacturer.ManufacturerId);

        if (currentIndex != -1)
        {
            _manufacturers[currentIndex] = manufacturer;
        }

        ManufacturerUpdated?.Invoke(manufacturer);
    }
}
