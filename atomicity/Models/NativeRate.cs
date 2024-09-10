using System;

namespace atomicity.Models;

public class NativeRate
{
    public DateTime Time { get; set; }
    public string Symbol { get; set; }
    public double Bid { get; set;}
    public double Ask { get; set;}

    public NativeRate(DateTime time, string symbol, double bid, double ask)
    {
        Time = time;
        Symbol = symbol;
        Bid = bid;
        Ask = ask;
    }
}
