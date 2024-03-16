using System.Windows.Input;

namespace PCPartsStore.WPF.ViewModels;

public class ModalButtonControlViewModel : ViewModelBase
{
    public ModalButtonControlViewModel(bool isEditable, ICommand addCommand, ICommand closeModalCommand, ICommand editCommand)
    {
        IsEditable = isEditable;
        IsAddable = !isEditable;
        AddCommand = addCommand;
        CloseModalCommand = closeModalCommand;
        EditCommand = editCommand;
    }

    public bool IsAddable { get; set; } = true;

    public bool IsEditable { get; set; } = false;

    public ICommand AddCommand { get; set; }

    public ICommand CloseModalCommand { get; set; }

    public ICommand EditCommand { get; set; }
}
