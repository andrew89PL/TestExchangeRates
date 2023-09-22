using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestExchangeRates.Database;
using TestExchangeRates.Database.Entities;
using TestExchangeRates.Models;

namespace TestExchangeRates.Logic
{
    public class ExchangeRatesManager
    {
        private static readonly string nbpAPI = "http://api.nbp.pl/api/exchangerates/tables/";

        private readonly DatabaseContext databaseContext;
        private readonly IHttpClientFactory httpClientFactory;
        private bool allowUpdates;

        private List<ExchangeRatesTableDTO> response;

        public ExchangeRatesManager(DatabaseContext databaseContext, IHttpClientFactory httpClientFactory, bool allowUpdates = true)
        {
            this.databaseContext = databaseContext;
            this.httpClientFactory = httpClientFactory;
            this.allowUpdates = allowUpdates;
        }

        public async Task<List<ExchangeRatesTableDTO>> GetLatestRates(string tableType)
        {
            await GetDataFromNBP(tableType);
            await SaveOrUpdate();

            return response;
        }

        private async Task GetDataFromNBP(string tableType)
        {
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(nbpAPI + tableType);
            response.EnsureSuccessStatusCode();
            var responseStringJson = await response.Content.ReadAsStringAsync();

            this.response = JsonConvert.DeserializeObject<List<ExchangeRatesTableDTO>>(responseStringJson) ?? new List<ExchangeRatesTableDTO>();
        }

        private async Task SaveOrUpdate()
        {
            foreach(var rateTable in response)
            {
                var exchangeTable = await GetExchageTable(rateTable.No);
                if (exchangeTable != null && !allowUpdates) continue;

                if (exchangeTable == null) exchangeTable = rateTable.ToEntity();
                else databaseContext.Rates.RemoveRange(exchangeTable.Rates);

                var newRates = rateTable.Rates
                    .Select(x =>
                    {
                        var rate = x.ToEntity();
                        rate.Currency = GetCurrency(x.Code, x.Currency);
                        rate.ExchangeRatesTable = exchangeTable;
                        return rate;
                    })
                    .ToList();
                databaseContext.AddRange(newRates);
            }
            await databaseContext.SaveChangesAsync();
        }

        private async Task<ExchangeRatesTable?> GetExchageTable(string tableNumber)
        {
            return await databaseContext.ExchangeRatesTables
                .Include(x => x.Rates)
                .ThenInclude(x => x.Currency)
                .SingleOrDefaultAsync(x => x.TableNumber == tableNumber);
        }

        private Currency GetCurrency(string code, string name)
        {
            return databaseContext.Currencies
                .SingleOrDefault(x => x.Code == code) ?? new Currency(code, name);
        }
    }
}
