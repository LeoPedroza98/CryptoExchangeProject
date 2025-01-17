namespace RateService.Domain.Entities;

public class ExchangeRateHistory
{
    public Guid Id { get; private set; }
    public DateTime Timestamp { get; set; }        
    public decimal OpenPrice { get; set; }      
    public decimal HighPrice { get; set; }        
    public decimal LowPrice { get; set; }           
    public decimal ClosePrice { get; set; }        
    public decimal Volume { get; set; }             
    public string ExchangeName { get; set; }        
    public string BaseCurrency { get; set; }        
    public string QuoteCurrency { get; set; }  
}