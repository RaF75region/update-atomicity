using System;
using System.Collections.Concurrent;
using atomicity.Models;

namespace atomicity.Repositories;

public class RatesStorageConcurrentDictionary
{
    private ConcurrentDictionary<string, Rate> rates = new();

    public void UpdateRate(NativeRate newRate)
    {
        rates.AddOrUpdate(
            newRate.Symbol, 
            symbol => new Rate(newRate.Time, newRate.Symbol, newRate.Bid, newRate.Ask),
            (symbol, oldRate) => 
            {
                oldRate.Time = newRate.Time;
                oldRate.Bid = newRate.Bid;
                oldRate.Ask = newRate.Ask;
                return oldRate;
            });
    }

    public Rate GetRate(string symbol)
    {
        rates.TryGetValue(symbol, out var rate);
        return rate;
    }
}
