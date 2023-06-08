using Microsoft.Maui.Controls;
using Shared.Models;


namespace MauiScientificCalculator.ViewModels;

[INotifyPropertyChanged]
public partial class HistoryPageViewModel
{
    public List<History> History { get; set; }


    public HistoryPageViewModel(BackendService backendService)
    {
        History = backendService.GetHistory().History;

    }
}