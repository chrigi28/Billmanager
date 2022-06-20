using System;
using System.Collections.Generic;

namespace Converter;

public class RechnungToBill
{
    public string Nettopreis { get; set; }
    public string Gesamttotal { get; set; }
    public string KmStand { get; set; }
    public string Reparatur { get; set; }
    public string Datum { get; set; }
    public string Bemerkungen { get; set; }
    public bool Bezahlt { get; set; }
    public string Rechnungsart { get; set; }
    public string BetreffendesFahrzeug { get; set; }
    public string Kunde { get; set; }
    public bool Steuer { get; set; }
    public List<List<string>> Auflistung { get; set; }
}