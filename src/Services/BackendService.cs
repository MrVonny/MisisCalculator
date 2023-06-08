using System.Linq;
using CalcaulatorBackend.Models;
using Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using static Android.Provider.Settings;

namespace MauiScientificCalculator
{
    public class BackendService
    {
        private readonly HttpClient _httpClient;
        private readonly string _id;

        public BackendService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _id = Secure.GetString(Android.App.Application.Context.ContentResolver, Secure.AndroidId);
        }

        public ExpressionResponseDto Calculate(string expr)
        {
            var response = _httpClient.PostAsJsonAsync("/Calculator", new ExpressionRequestDto()
            {
                Expression = expr,
                DeviceName = _id
            }).Result;

            return response.Content.ReadFromJsonAsync<ExpressionResponseDto>().Result;
        }

        public HistoryResponseDto GetHistory()
        {
            var response = _httpClient.PostAsJsonAsync("/Calculator/history", new HistoryRequestDto()
            {
                DeviceName = _id
            }).Result;

            var data = response.Content.ReadFromJsonAsync<HistoryResponseDto>().Result;
            
            foreach (var h in data.History)
            {
                h.CreatedAt = h.CreatedAt.ToLocalTime();
            }

            data.History = data.History.OrderByDescending(x => x.CreatedAt).ToList();

            return data;
        }
    }
}
