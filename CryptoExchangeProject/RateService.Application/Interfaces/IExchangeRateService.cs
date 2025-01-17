using RateService.Application.DTOs;

namespace RateService.Application.Interfaces;

public interface IExchangeRateService
{
    Task<IEnumerable<ExchangeRateDto>> GetBestRatesAsync(CurrencyPairDto pairDto);
    Task<IEnumerable<ExchangeRateHistoryDto>> GetRateHistoryAsync(RateHistoryRequestDto pairDto);
}