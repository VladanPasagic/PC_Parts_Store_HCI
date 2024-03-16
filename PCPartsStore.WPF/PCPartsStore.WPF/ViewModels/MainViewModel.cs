using PCPartsStore.WPF.Stores;

namespace PCPartsStore.WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel => NavigationStore.Instance.CurrentViewModel!;

    public MainViewModel()
    {
        NavigationStore.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
