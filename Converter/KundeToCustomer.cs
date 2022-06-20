using System.Collections.Generic;

namespace Converter
{
    public class KundeToCustomer
    {
        public string NameVorname{ get;set;}
        public string Strasse{ get;set;}
        public string PlzOrt{ get;set;}
        public string TelNr{ get;set;}
        public string Anrede{ get;set;}
        public List<FahrzeugToVehicle> Fahrzeuge { get; set; }
        public List<RechnungToBill> Rechnungen { get; set; }
    }
}