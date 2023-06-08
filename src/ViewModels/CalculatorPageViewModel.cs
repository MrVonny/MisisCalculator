using System.Net.Http;
using System.Net.Http.Json;
using CalcaulatorBackend.Models;
using Shared.Models;

namespace MauiScientificCalculator.ViewModels;

[INotifyPropertyChanged]
public partial class CalculatorPageViewModel
{
    private readonly BackendService _backendService;

    [ObservableProperty]
    private string inputText = "";

    [ObservableProperty]
    private string calculatedResult = "0";

    private bool isSciOpWaiting = false;

    public CalculatorPageViewModel(BackendService backendService)
    {
        _backendService = backendService;
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
            var result = _backendService.Calculate(InputText);
            CalculatedResult = result.Success ? result.Result.ToString() : "Error";
        }
        catch (Exception)
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
