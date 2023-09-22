using Microsoft.EntityFrameworkCore;

namespace TestExchangeRates.Database.Entities
{
    public class Currency
    {
        public int Id { get; set; }

        [Comment("Code – kod waluty")]
        public string Code { get; set; }

        [Comment("Currency – nazwa waluty")]
        public string Name { get; set; }

        public List<Rate> Rates { get; set; }

        public Currency(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
