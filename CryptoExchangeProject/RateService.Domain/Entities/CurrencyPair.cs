namespace RateService.Domain.Entities;

public class CurrencyPair
{
    public string BaseCurrency { get; }
    public string QuoteCurrency { get; }

    public CurrencyPair(string baseCurrency, string quoteCurrency)
    {
        if (string.IsNullOrWhiteSpace(baseCurrency) || string.IsNullOrWhiteSpace(quoteCurrency))
            throw new ArgumentException("Currencies cannot be empty");

        BaseCurrency = baseCurrency.ToUpper();
        QuoteCurrency = quoteCurrency.ToUpper();
    }

    public override bool Equals(object obj)
    {
        return obj is CurrencyPair pair &&
               BaseCurrency == pair.BaseCurrency &&
               QuoteCurrency == pair.QuoteCurrency;
    }

    public override int GetHashCode() => HashCode.Combine(BaseCurrency, QuoteCurrency);
}