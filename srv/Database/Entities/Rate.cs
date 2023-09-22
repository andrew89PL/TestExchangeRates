using Microsoft.EntityFrameworkCore;

namespace TestExchangeRates.Database.Entities
{
    public class Rate
    {
        public int Id { get; set; }

        [Comment("Przeliczony kurs średni waluty (dotyczy tabel A oraz B)")]
        public decimal? Mid { get; set; }

        [Comment("Przeliczony kurs kupna waluty (dotyczy tabeli C)")]
        public decimal? Bid { get; set; }

        [Comment("Przeliczony kurs sprzedaży waluty (dotyczy tabeli C)")]
        public decimal? Ask { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int ExchangeRatesTableId { get; set; }
        public ExchangeRatesTable ExchangeRatesTable { get; set; }
    }
}
