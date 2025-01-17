namespace RateService.Application.DTOs;

public class ExchangeRateDto
{
    public string ExchangeName { get; set; }
    public string BaseCurrency { get; set; }
    public string QuoteCurrency { get; set; }
    public decimal Rate { get; set; }
    public DateTime RetrievedAt { get; set; }
}