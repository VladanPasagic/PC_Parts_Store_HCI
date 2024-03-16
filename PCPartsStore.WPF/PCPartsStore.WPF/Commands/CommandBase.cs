using System.Windows.Input;

namespace PCPartsStore.WPF.Commands;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    public abstract void Execute(object? parameter);

    protected virtual void OnCanExecuteChanged(object? parameter = default)
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
