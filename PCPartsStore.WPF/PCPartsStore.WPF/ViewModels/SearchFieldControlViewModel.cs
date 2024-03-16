using PCPartsStore.WPF.Commands;

namespace PCPartsStore.WPF.ViewModels;

public class SearchFieldControlViewModel : ViewModelBase
{
    public string? SearchText { get; set; }

    public SearchCommand SearchCommand { get; }

    public QuitSearchCommand QuitSearchCommand { get; }

    private bool _hasSearched = false;

    public bool HasSearched
    {
        get => _hasSearched;
        set
        {
            _hasSearched = value;
            OnPropertyChanged(nameof(HasSearched));
        }
    }
    private bool _hasNotSearched = true;
    public bool HasNotSearched
    {
        get => _hasNotSearched;
        set
        {
            _hasNotSearched = value;
            OnPropertyChanged(nameof(HasNotSearched));
        }
    }

    public SearchFieldControlViewModel(SearchCommand searchCommand, QuitSearchCommand quitSearchCommand)
    {
        SearchCommand = searchCommand;
        SearchCommand.AddInnerViewModel(this);
        QuitSearchCommand = quitSearchCommand;
        quitSearchCommand.AddInnerViewModel(this);
    }
}
