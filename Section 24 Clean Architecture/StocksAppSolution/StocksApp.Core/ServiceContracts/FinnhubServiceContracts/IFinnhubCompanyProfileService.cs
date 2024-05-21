namespace ServiceContracts.FinnhubServiceContracts
{
    public interface IFinnhubCompanyProfileService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}