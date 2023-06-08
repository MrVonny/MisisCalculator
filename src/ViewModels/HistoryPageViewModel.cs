using Shared.Models;


namespace MauiScientificCalculator.ViewModels;

[INotifyPropertyChanged]
public partial class HistoryPageViewModel
{
    private readonly BackendService _backendService;

    [ObservableProperty] 
    private List<History> history;


    public HistoryPageViewModel(BackendService backendService)
    {
        _backendService = backendService;
        Refresh();
    }

    [RelayCommand]
    private void Refresh()
    {
        History = _backendService.GetHistory().History;
    }
}