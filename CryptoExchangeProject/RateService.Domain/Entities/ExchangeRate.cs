namespace RateService.Domain.Entities;

public class ExchangeRate
{
    public Guid Id { get; private set; }
    public string ExchangeName { get; private set; }
    public string BaseCurrency { get; private set; }
    public string QuoteCurrency { get; private set; }
    public decimal Rate { get; private set; }
    public DateTime RetrievedAt { get; private set; }

    public ExchangeRate(string exchangeName, string baseCurrency, string quoteCurrency, decimal rate, DateTime retrievedAt)
    {
        Id = Guid.NewGuid();
        ExchangeName = exchangeName;
        BaseCurrency = baseCurrency;
        QuoteCurrency = quoteCurrency;
        Rate = rate;
        RetrievedAt = retrievedAt;
    }
}