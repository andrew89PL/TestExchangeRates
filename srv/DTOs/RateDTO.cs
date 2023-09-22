using TestExchangeRates.Database.Entities;

namespace TestExchangeRates.Models
{
    public class RateDTO
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public decimal? Mid { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Ask { get; set; }

        public RateDTO() { }

        public RateDTO(Rate rate)
        {
            FromEntity(rate);
        }

        public void FromEntity(Rate rate)
        {
            Currency = rate.Currency.Name;
            Code = rate.Currency.Code;
            Mid = rate.Mid;
            Bid = rate.Bid;
            Ask = rate.Ask;
        }

        public Rate ToEntity()
        {
            return new Rate
            {
                Mid = Mid,
                Bid = Bid,
                Ask = Ask
            };
        }
    }
}
