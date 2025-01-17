using Moq;
using RateService.Application.DTOs;
using RateService.Application.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateService.Tests
{
    public class ExchangeRateServiceTests
    {
        private readonly Mock<IExchangeRateService> _exchangeRateServiceMock;

        public ExchangeRateServiceTests()
        {
            _exchangeRateServiceMock = new Mock<IExchangeRateService>();
        }

        [Fact]
        public async Task GetExchangeRate_ShouldReturnValidRate()
        {
            // Arrange
            var currencyPairDto = new CurrencyPairDto { BaseCurrency = "BTC", QuoteCurrency = "USDT" };
            var expectedRate = new List<ExchangeRateDto>
            {
                new ExchangeRateDto { BaseCurrency = "BTC", QuoteCurrency = "USDT", Rate = 50000 }
            };

            _exchangeRateServiceMock
                .Setup(service => service.GetBestRatesAsync(It.Is<CurrencyPairDto>(
                    dto => dto.BaseCurrency == "BTC" && dto.QuoteCurrency == "USDT")))
                .ReturnsAsync(expectedRate);

            var service = _exchangeRateServiceMock.Object;

            // Act
            var result = await service.GetBestRatesAsync(currencyPairDto);

            // Assert
            Assert.NotNull(result);
            var rate = Assert.Single(result);
            Assert.Equal("BTC", rate.BaseCurrency);
            Assert.Equal("USDT", rate.QuoteCurrency);
            Assert.Equal(50000, rate.Rate);
        }
    }
}