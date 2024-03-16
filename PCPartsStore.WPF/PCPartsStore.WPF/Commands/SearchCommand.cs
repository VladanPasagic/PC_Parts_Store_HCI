using PCPartsStore.WPF.ViewModels;
using PCPartsStore.WPF.ViewModels.Interfaces;

namespace PCPartsStore.WPF.Commands;

public class SearchCommand : CommandBase
{
    private readonly ISearchViewModel _searchViewModel;
    private SearchFieldControlViewModel? _searchFieldControlViewModel;

    public SearchCommand(ISearchViewModel searchViewModel)
    {
        _searchViewModel = searchViewModel;
    }

    public void AddInnerViewModel(SearchFieldControlViewModel viewModel)
    {
        _searchFieldControlViewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        if (_searchFieldControlViewModel != null)
        {
            _searchFieldControlViewModel.HasSearched = true;
            _searchFieldControlViewModel.HasNotSearched = false;
            _searchViewModel.Search(_searchFieldControlViewModel.SearchText ?? "");
        }
    }
}
