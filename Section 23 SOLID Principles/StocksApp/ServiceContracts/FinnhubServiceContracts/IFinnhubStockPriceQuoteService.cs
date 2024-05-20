﻿namespace ServiceContracts.FinnhubServiceContracts
{
    public interface IFinnhubStockPriceQuoteService
    {
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}