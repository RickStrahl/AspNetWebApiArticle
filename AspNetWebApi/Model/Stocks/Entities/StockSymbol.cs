using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class StockSymbol
{
    public int Id { get; set; }

    [StringLength(10)]
    public string Symbol { get; set; }

    [StringLength(100)]
    public string Company { get; set; }
}