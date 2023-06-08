namespace MauiScientificCalculator.Views;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage(CalculatorPageViewModel calculatorPageViewModel)
	{
		InitializeComponent();

        //Initialize the view model
        this.BindingContext = calculatorPageViewModel;
    }
}