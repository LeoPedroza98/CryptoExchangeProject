using Microsoft.AspNetCore.Mvc;
using RateService.Application.DTOs;
using RateService.Application.Interfaces;

namespace RateService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }
    
    [HttpGet("cryptocurrencies")]
    public IActionResult GetAvailableCryptocurrencies()
    {
        var cryptocurrencies = new List<CurrencyDto>
        {
            new CurrencyDto { Symbol = "BTC", Name = "Bitcoin" },
            new CurrencyDto { Symbol = "ETH", Name = "Ethereum" },
            new CurrencyDto { Symbol = "ETC", Name = "Ethereum Classic" },
            new CurrencyDto { Symbol = "XMR", Name = "Monero" },
            new CurrencyDto { Symbol = "ZEC", Name = "Zcash" },
            new CurrencyDto { Symbol = "BCH", Name = "Bitcoin Cash" },
            new CurrencyDto { Symbol = "USDT", Name = "Tether" },
            new CurrencyDto { Symbol = "BUSD", Name = "Binance USD" },
            new CurrencyDto { Symbol = "USDC", Name = "USD Coin" },
            new CurrencyDto { Symbol = "DAI", Name = "DAI" },
        };

        return Ok(cryptocurrencies);
    }

    [HttpGet("best-rates")]
    public async Task<IActionResult> GetBestRates([FromQuery] string baseCurrency, [FromQuery] string quoteCurrency)
    {
        if (string.IsNullOrWhiteSpace(baseCurrency) || string.IsNullOrWhiteSpace(quoteCurrency))
            return BadRequest("BaseCurrency and QuoteCurrency are required.");

        var pairDto = new CurrencyPairDto { BaseCurrency = baseCurrency, QuoteCurrency = quoteCurrency };
        var rates = await _exchangeRateService.GetBestRatesAsync(pairDto);
        return Ok(rates);
    }
    
    [HttpGet("rate-history")]
    public async Task<IActionResult> GetRateHistory([FromQuery] string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return BadRequest("O símbolo é obrigatório. Exemplo: BTCUSDT");

        var pairDto = new RateHistoryRequestDto() { Symbol = symbol };
        var history = await _exchangeRateService.GetRateHistoryAsync(pairDto);
        return Ok(history);
    }
}