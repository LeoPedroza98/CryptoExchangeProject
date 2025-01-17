using Newtonsoft.Json;
using RateService.Application.DTOs;
using RateService.Application.Interfaces;

public class ExchangeRateAppService : IExchangeRateService
{
    private readonly IEnumerable<IExchangeRateService> _exchangeIntegrations;
    private readonly ICacheService _cacheService;

    public ExchangeRateAppService(IEnumerable<IExchangeRateService> exchangeIntegrations, ICacheService cacheService)
    {
        _exchangeIntegrations = exchangeIntegrations;
        _cacheService = cacheService;
    }


    public ExchangeRateAppService()
    {
    }

    public async Task<IEnumerable<ExchangeRateDto>> GetBestRatesAsync(CurrencyPairDto pairDto)
    {
        var cacheKey = $"rate:{pairDto.BaseCurrency}-{pairDto.QuoteCurrency}";
        var cachedRates = await _cacheService.GetAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(cachedRates))
            return JsonConvert.DeserializeObject<IEnumerable<ExchangeRateDto>>(cachedRates);

        var ratesTasks = _exchangeIntegrations.Select(service => service.GetBestRatesAsync(pairDto));
        var ratesResults = await Task.WhenAll(ratesTasks);

        var allRates = ratesResults.SelectMany(r => r).ToList();

        await _cacheService.SetAsync(cacheKey, JsonConvert.SerializeObject(allRates), TimeSpan.FromMinutes(5));
        return allRates;
    }

    public async Task<IEnumerable<ExchangeRateHistoryDto>> GetRateHistoryAsync(RateHistoryRequestDto pairDto)
    {
        var cacheKey = $"rate-history:{pairDto.Symbol}";
        var cachedHistory = await _cacheService.GetAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(cachedHistory))
            return JsonConvert.DeserializeObject<IEnumerable<ExchangeRateHistoryDto>>(cachedHistory);

        var historyTasks = _exchangeIntegrations.Select(service => service.GetRateHistoryAsync(pairDto));
        var historyResults = await Task.WhenAll(historyTasks);

        var allHistory = historyResults.SelectMany(h => h).ToList();

        await _cacheService.SetAsync(cacheKey, JsonConvert.SerializeObject(allHistory), TimeSpan.FromMinutes(10));
        return allHistory;
    }
}