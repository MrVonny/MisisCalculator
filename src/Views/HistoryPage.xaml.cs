namespace MauiScientificCalculator.Views;

public partial class HistoryPage : ContentPage
{
	public HistoryPage(HistoryPageViewModel historyPageViewModel)
	{
		InitializeComponent();
        BindingContext = historyPageViewModel;
    }
}