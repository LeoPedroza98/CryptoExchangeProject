using Newtonsoft.Json;
using RateService.Application.DTOs;
using RateService.Application.Interfaces;

public class BinanceIntegrationService : IExchangeRateService
{
    private readonly HttpClient _httpClient;

    public BinanceIntegrationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ExchangeRateDto>> GetBestRatesAsync(CurrencyPairDto pairDto)
    {
        var response = await _httpClient.GetAsync($"/api/v3/ticker/price?symbol={pairDto.BaseCurrency}{pairDto.QuoteCurrency}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BinancePriceResponse>(content);

        return new List<ExchangeRateDto>
        {
            new ExchangeRateDto
            {
                BaseCurrency = pairDto.BaseCurrency,
                QuoteCurrency = pairDto.QuoteCurrency,
                Rate = decimal.Parse(result.Price),
                RetrievedAt = DateTime.UtcNow,
                ExchangeName = "Binance"
            }
        };
    }

    public async Task<IEnumerable<ExchangeRateHistoryDto>> GetRateHistoryAsync(RateHistoryRequestDto pairDto)
    {
        var response = await _httpClient.GetAsync($"/api/v3/klines?symbol={pairDto.Symbol}&interval=1h&limit=24");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var rawData = JsonConvert.DeserializeObject<List<List<object>>>(content);

        return rawData.Select(item => new ExchangeRateHistoryDto
        {
            Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(item[0])).UtcDateTime,
            OpenPrice = Convert.ToDecimal(item[1]),
            HighPrice = Convert.ToDecimal(item[2]),
            LowPrice = Convert.ToDecimal(item[3]),
            ClosePrice = Convert.ToDecimal(item[4]),
            Volume = Convert.ToDecimal(item[5]),
            ExchangeName = "Binance"
        }).ToList();
    }

    private class BinancePriceResponse
    {
        [JsonProperty("price")]
        public string Price { get; set; }
    }
}
