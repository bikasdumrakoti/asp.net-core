namespace ServiceContracts.FinnhubServiceContracts
{
    public interface IFinnhubSearchStocksService
    {
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}