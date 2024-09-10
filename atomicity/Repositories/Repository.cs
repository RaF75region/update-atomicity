using System;
using atomicity.Models;

namespace atomicity.Repositories;

public class Repository
{
    private Dictionary<string, Rate> rates = new();
    private readonly object lockObj = new object();

    public void UpdateRate(NativeRate newRate)
    {
        lock (lockObj)
        {
            if (!rates.ContainsKey(newRate.Symbol))
            {
                rates.Add(newRate.Symbol, new Rate(newRate.Time, newRate.Symbol, newRate.Bid, newRate.Ask));
            }

            var oldRate = rates[newRate.Symbol];
            oldRate.Time = newRate.Time;
            oldRate.Bid = newRate.Bid;
            oldRate.Ask = newRate.Ask;
        }
    }
}
