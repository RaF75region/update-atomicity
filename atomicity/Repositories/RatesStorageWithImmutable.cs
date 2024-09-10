using System;
using atomicity.Models;

namespace atomicity.Repositories;

public class RatesStorageWithImmutable
{
    private Dictionary<string, Rate> rates = new();
    private readonly object lockObj = new object();

    public void UpdateRate(NativeRate newRate)
    {
        lock (lockObj)
        {
            rates[newRate.Symbol] = new Rate(newRate.Time, newRate.Symbol, newRate.Bid, newRate.Ask);
        }
    }

    public Rate GetRate(string symbol)
    {
        lock (lockObj)
        {
            if (!rates.ContainsKey(symbol))
                return null;
            return rates[symbol];
        }
    }
}
