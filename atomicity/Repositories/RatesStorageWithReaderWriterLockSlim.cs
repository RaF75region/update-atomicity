using System;
using atomicity.Models;

namespace atomicity.Repositories;

public class RatesStorageWithReaderWriterLockSlim
{
    private Dictionary<string, Rate> rates = new();
    private ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

    public void UpdateRate(NativeRate newRate)
    {
        rwLock.EnterWriteLock();
        try
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
        finally
        {
            rwLock.ExitWriteLock();
        }
    }

    public Rate GetRate(string symbol)
    {
        rwLock.EnterReadLock();
        try
        {
            if (!rates.ContainsKey(symbol))
                return null;
            return rates[symbol];
        }
        finally
        {
            rwLock.ExitReadLock();
        }
    }
}
