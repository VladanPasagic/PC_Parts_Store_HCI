namespace PCPartsStore.WPF.ViewModels.Interfaces;

public interface IErrorViewModel
{
    public string? ErrorMessage { get; set; }

    public bool HasErrorMessage { get; }
}
