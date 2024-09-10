using System;
using System.Diagnostics;
using atomicity.Models;
using atomicity.Repositories;

namespace atomicity.Tests;

public static class Test
{
    public static void TestMemoryUsage(string approachName, Action testMethod)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBefore = GC.GetTotalMemory(true);

        Stopwatch sw = Stopwatch.StartNew();
        testMethod();
        sw.Stop();

        long memoryAfter = GC.GetTotalMemory(true);

        Console.WriteLine($"{approachName}:");
        Console.WriteLine($"  Время выполнения: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"  Потребление памяти: {memoryAfter - memoryBefore} байт");
    }

    public static void TestLockApproach()
    {
         var storage = new RatesStorage();

        var rate1 = new NativeRate(DateTime.Now, "EURUSD", 1.1, 1.2 );
        var rate2 = new NativeRate(DateTime.Now, "EURUSD", 1.2, 1.3 );

        storage.UpdateRate(rate1);
        var result1 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result1.Bid}, Ask: {result1.Ask}");

        storage.UpdateRate(rate2);
        var result2 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result2.Bid}, Ask: {result2.Ask}");
    }

    public static void TestReaderWriterLockSlimApproach()
    {
        var storage = new RatesStorage();

        var rate1 = new NativeRate(DateTime.Now, "EURUSD", 1.1, 1.2 );
        var rate2 = new NativeRate(DateTime.Now, "EURUSD", 1.2, 1.3 );

        storage.UpdateRate(rate1);
        var result1 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result1.Bid}, Ask: {result1.Ask}");

        storage.UpdateRate(rate2);
        var result2 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result2.Bid}, Ask: {result2.Ask}");
    }

    public static void TestImmutableApproach()
    {
        var storage = new RatesStorage();

        var rate1 = new NativeRate(DateTime.Now, "EURUSD", 1.1, 1.2 );
        var rate2 = new NativeRate(DateTime.Now, "EURUSD", 1.2, 1.3 );

        storage.UpdateRate(rate1);
        var result1 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result1.Bid}, Ask: {result1.Ask}");

        storage.UpdateRate(rate2);
        var result2 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result2.Bid}, Ask: {result2.Ask}");
    }

    public static void TestConcurrentDictionaryApproach()
    {
        var storage = new RatesStorage();

        var rate1 = new NativeRate(DateTime.Now, "EURUSD", 1.1, 1.2 );
        var rate2 = new NativeRate(DateTime.Now, "EURUSD", 1.2, 1.3 );

        storage.UpdateRate(rate1);
        var result1 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result1.Bid}, Ask: {result1.Ask}");

        storage.UpdateRate(rate2);
        var result2 = storage.GetRate("EURUSD");
        Console.WriteLine($"Bid: {result2.Bid}, Ask: {result2.Ask}");
    }
}
