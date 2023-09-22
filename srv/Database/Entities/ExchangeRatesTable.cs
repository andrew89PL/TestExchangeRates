using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace TestExchangeRates.Database.Entities
{
    public class ExchangeRatesTable
    {
        public int Id { get; set; }

        [Comment("NumerTabeli")]
        public string TableNumber { get; set; }

        [Comment("Typ tabeli")]
        public ExchangeTableType TableType { get; set; }

        [Comment("Data publikacji")]
        public DateTime EffectiveDate { get; set; }

        [Comment("Data notowania (dotyczy tabeli C)")]
        public DateTime? TradingDate { get; set;}

        [Comment("Lista kursów poszczególnych walut w tabeli")]
        public List<Rate> Rates { get; set; }
    }

    public enum ExchangeTableType
    {
        [Description("Tabela A kursów średnich walut obcych")]
        A = 'A',
        [Description("Tabela B kursów średnich walut obcych")]
        B = 'B',
        [Description("Tabela C kursów kupna i sprzedaży walut obcych")]
        C = 'C'
    }
}
