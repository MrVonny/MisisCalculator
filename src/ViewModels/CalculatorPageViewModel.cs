using System.Net.Http;
using System.Net.Http.Json;
using CalcaulatorBackend.Models;

namespace MauiScientificCalculator.ViewModels;

[INotifyPropertyChanged]
internal partial class CalculatorPageViewModel
{
    [ObservableProperty]
    private string inputText = "";

    [ObservableProperty]
    private string calculatedResult = "0";

    private bool isSciOpWaiting = false;

    public CalculatorPageViewModel()
    {
    }

    [RelayCommand]
    private void Reset()
    {
        CalculatedResult = "0";
        InputText = "";
        isSciOpWaiting = false;
    }

    [RelayCommand]
    private void Calculate()
    {
        if (InputText.Length == 0)
            return;

        if (isSciOpWaiting)
        {
            InputText += ")";
            isSciOpWaiting = false;
        }

        try
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://77.223.107.117/")
            };

            var response = httpClient.PostAsJsonAsync("/Calculator", new ExpressionRequestDto()
            {
                Expression = InputText
            }).Result;

            var result = response.Content.ReadFromJsonAsync<ExpressionResponseDto>().Result;

            CalculatedResult = result.Success ? result.Result.ToString() : "Error";
        }
        catch (Exception ex)
        {
            CalculatedResult = "NaN";
        }
    }


    [RelayCommand]
    private void Backspace()
    {
        if (InputText.Length > 0)
            InputText = InputText.Substring(0, InputText.Length - 1);
    }

    [RelayCommand]
    private void NumberInput(string key)
    {
        InputText += key;
    }

    [RelayCommand]
    private void MathOperator(string op)
    {
        if (isSciOpWaiting)
        {
            InputText += ")";
            isSciOpWaiting = false;
        }

        InputText += $" {op} ";
    }

    [RelayCommand]
    private void RegionOperator(string op)
    {
        if (op == ")")
            isSciOpWaiting = false;

        InputText += op;
    }

    [RelayCommand]
    private void ScientificOperator(string op)
    {
        InputText += $"{op}(";
        isSciOpWaiting = true;
    }

}
