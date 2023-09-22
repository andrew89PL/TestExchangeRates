using TestExchangeRates.Database.Entities;

namespace TestExchangeRates.Models
{
    public class ExchangeRatesTableDTO
    {
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? TradingDate { get; set; }

        public List<RateDTO> Rates { get; set; }

        public ExchangeRatesTableDTO() { }

        public ExchangeRatesTableDTO(ExchangeRatesTable table)
        {
            FromEntity(table);
        }

        public void FromEntity(ExchangeRatesTable table)
        {
            Table = table.TableType.ToString();
            No = table.TableNumber;
            EffectiveDate = table.EffectiveDate;
            TradingDate = table.TradingDate;
        }

        public ExchangeRatesTable ToEntity()
        {
            return new ExchangeRatesTable
            {
                TableType = GetTableType(),
                TableNumber = No,
                EffectiveDate = EffectiveDate,
                TradingDate = TradingDate
            };
        }

        private ExchangeTableType GetTableType()
        {
            return Table switch
            {
                "A" => ExchangeTableType.A,
                "B" => ExchangeTableType.B,
                "C" => ExchangeTableType.C,
                _ => throw new Exception("Brak podanego popawnego typu tabeli")
            };
        }
    }
}
