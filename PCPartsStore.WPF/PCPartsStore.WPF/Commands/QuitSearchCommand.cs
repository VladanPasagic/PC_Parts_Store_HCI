using PCPartsStore.WPF.ViewModels;
using PCPartsStore.WPF.ViewModels.Interfaces;

namespace PCPartsStore.WPF.Commands;

public class QuitSearchCommand : CommandBase
{
    private readonly ISearchViewModel _searchViewModel;
    private SearchFieldControlViewModel? _searchFieldControlViewModel;

    public QuitSearchCommand(ISearchViewModel searchViewModel)
    {
        _searchViewModel = searchViewModel;
    }

    public override void Execute(object? parameter)
    {
        if (_searchFieldControlViewModel !=null)
        {
            _searchFieldControlViewModel.HasNotSearched = true;
            _searchFieldControlViewModel.HasSearched = false;
            _searchViewModel.QuitSearch();
        }
    }

    public void AddInnerViewModel(SearchFieldControlViewModel viewModel)
    {
        _searchFieldControlViewModel = viewModel;
    }
}
