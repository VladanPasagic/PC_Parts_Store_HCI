using PCPartsStore.WPF.ViewModels;

namespace PCPartsStore.WPF.Stores;

public class NavigationStore
{
    public static NavigationStore Instance { get; } = new();

    public event Action? CurrentViewModelChanged;

    private ViewModelBase? _currentViewModel;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
