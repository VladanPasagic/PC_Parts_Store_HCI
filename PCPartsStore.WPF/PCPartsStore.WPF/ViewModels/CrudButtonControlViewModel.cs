using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class CrudButtonControlViewModel : ViewModelBase
{
    public CrudButtonControlViewModel(ICommand create, ICommand delete, ICommand update)
    {
        Create = create;
        Delete = delete;
        Update = update;
    }

    public ICommand Create { get; }

    public ICommand Delete { get; }

    public ICommand Update { get; }
}
