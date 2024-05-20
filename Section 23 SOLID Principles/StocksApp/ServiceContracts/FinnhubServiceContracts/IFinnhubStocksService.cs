namespace ServiceContracts.FinnhubServiceContracts
{
    public interface IFinnhubStocksService
    {
        Task<List<Dictionary<string, string>>?> GetStocks();
    }
}