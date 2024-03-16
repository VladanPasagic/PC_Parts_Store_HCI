namespace PCPartsStore.WPF.ViewModels.Interfaces;

public interface ISearchViewModel
{
    SearchFieldControlViewModel SearchFieldControl { get; }
    void Search(string searchString);

    void QuitSearch();
}
