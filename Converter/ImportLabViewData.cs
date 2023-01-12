using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billmanager.Database;
using Billmanager.Database.Tables;
using Newtonsoft.Json;

namespace Converter
{
    public class ImportLabViewData
    {
        static List<DirectoryInfo> folders = new List<DirectoryInfo>(); // List that hold direcotries that cannot be accessed


        static void Main(string[] args)
        {
            var importer = new ImportLabViewData();
            importer.InitiateImport();
        }

        public void InitiateImport()
        {
            var path = "D:\\Rechnung\\übernahme kunden\\convertedRechnungsdata";
            var kundenDaten = GetKundenFromPath(path);
            ConvertCustomerToDbt(kundenDaten);
        }

        private static async void ConvertCustomerToDbt(List<KundeToCustomer> kundenDaten)
        {
            using (var db = SqliteDatabase.AssureDbImport())
            {
                foreach (var currentData in kundenDaten)
                {
                    string vorname = string.Empty;
                    string nachname = string.Empty;
                    var nameSplit = currentData.NameVorname.Split(' ');
                    ////switch (nameSplit.Length)
                    ////{
                    ////    case 0:
                    ////        vorname = "ERROR No Name detected";
                    ////        break;
                    ////    case 1:
                    ////        vorname = nameSplit[0];
                    ////        break;
                    ////    case 2:
                    ////        vorname = nameSplit[0];
                    ////        nachname = nameSplit[1];
                    ////        break;
                    ////    default:
                    ////        vorname = nameSplit[0];
                    ////        bool first = true;
                    ////        foreach (var split in nameSplit)
                    ////        {
                    ////            if (first)
                    ////            {
                    ////                first = false;
                    ////                continue;
                    ////            }

                    ////            nachname += $"{split} ";
                    ////        }

                    ////        break;
                    ////}


                    var zipSplit = currentData.PlzOrt.Split(' ');

                    var customer = new CustomerDbt()
                    {
                        Address = currentData.Strasse,
                        FirstName = currentData.NameVorname,
                        ////LastName = nachname,
                        Location = zipSplit[1],
                        Zip = zipSplit[0],
                        Phone = currentData.TelNr,
                        Title_customer = currentData.Anrede,
                    };

                    await SqliteDatabase.AssureDbImport().AddItemAsync(customer).ConfigureAwait(false); 

                    foreach (var fhz in currentData.Fahrzeuge)
                    {
                        var car = new CarDbt()
                        {
                            CarMake = fhz.Marke,
                            ChassisNo = fhz.ChassisNr,
                            Cubic = fhz.Hubraum,
                            EnginNo = fhz.MotorNr,
                            Plate = fhz.Kontrollschild,
                            FirstOnMarket = fhz.ErstVerkehr,
                            Rootnumber = fhz.StammNr,
                            Typ = fhz.Typ,
                            Typecertificate = fhz.Typenschein,
                            CustomerId = customer.Id,
                            Customer = customer
                        };

                        await SqliteDatabase.AssureDbImport().AddItemAsync(car).ConfigureAwait(false);
                    } 

                    foreach (var bll in currentData.Rechnungen)
                    {
                        var bill = new BillDbt()
                        {
                            CustomerId = customer.Id,
                            Customer = customer,
                            ////Car = cu,
                            ////CarId =
                            Conclusion = bll.Reparatur,
                            Date = DateTime.Parse(bll.Datum),
                            Kilometers = int.Parse(bll.KmStand),
                            NettoPrice = decimal.Parse(bll.Nettopreis),
                            Payed = bll.Bezahlt,
                            Remark = bll.Bemerkungen,
                        };
                        bill.ItemPositions = new ObservableCollection<ItemPositionDbt>(bll.Auflistung.Select(f =>
                            new ItemPositionDbt
                            {
                                Bill = bill,
                                Description = f[0],
                                Amount = decimal.Parse(f[1]),
                                Price = decimal.Parse(f[2])
                            }));

                        await SqliteDatabase.AssureDbImport().AddItemAsync(bill).ConfigureAwait(false);
                    } 
                }
            }
        }

        private static List<KundeToCustomer> GetKundenFromPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            var customerFiles = FullDirList(di, "*.knd.txt");
            ////Console.WriteLine("Done");
            List<KundeToCustomer> kunden = new List<KundeToCustomer>();
            foreach (var customer in customerFiles)
            {
                List<FahrzeugToVehicle> fahrzeuge = new();
                List<RechnungToBill> rechnungen = new();
                //Console.WriteLine($"{customer.Name}");
                var customerObject = LoadCustomerJson(customer.FullName);
                if (customerObject != null)
                {
                    kunden.Add(customerObject);
                    Console.WriteLine($"{customerObject.Anrede} {customerObject.NameVorname} {customerObject.Strasse} {customerObject.PlzOrt} {customerObject.TelNr}");
                    fahrzeuge = GetVehicleFromCustomerFolder(customer.DirectoryName);
                    rechnungen = GetBillFromCustomerFolder(customer.DirectoryName);
                    customerObject.Fahrzeuge = fahrzeuge;
                    customerObject.Rechnungen = rechnungen;
                }
                else
                {
                    Console.WriteLine($"Failed {customer.FullName}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            }

            return kunden;
        }

        private static List<FahrzeugToVehicle> GetVehicleFromCustomerFolder(string customerDirectory)
        {
            string fhzString = @"fahrzeuge";
            DirectoryInfo di;
            di = new DirectoryInfo(Path.Join(customerDirectory, fhzString));
            var fhzFiles = FullDirList(di, "*.fhz.txt");
            
            List<FahrzeugToVehicle> fahrzeuge = new();
            foreach (var fhz in fhzFiles)
            {
                var fhzObject = LoadVehicleJson(fhz.FullName);
                if (fhzObject != null)
                {
                    fahrzeuge.Add(fhzObject);
                }
                else
                {
                    Console.WriteLine($"Failed {fhz.FullName}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            }

            return fahrzeuge;
        }

        private static List<RechnungToBill> GetBillFromCustomerFolder(string customerDirectory)
        {
            DirectoryInfo di;
            string bll = @"rechnungen";
            di = new DirectoryInfo(Path.Join(customerDirectory, bll));
            var billFiles = FullDirList(di, "*.bll.txt");
            
            List<RechnungToBill> rechnungen = new();
            foreach (var bill in billFiles)
            {
                var rechObject = LoadBillJson(bill.FullName);
                if (rechObject != null)
                {
                    rechnungen.Add(rechObject);
                }
                else
                {
                    Console.WriteLine($"Failed {bill.FullName}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            }

            return rechnungen;
        }

        static KundeToCustomer? LoadCustomerJson(string path)
        {
            KundeToCustomer? customer;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<KundeToCustomer>(json);
            }

            return null;
        }

        static FahrzeugToVehicle? LoadVehicleJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<FahrzeugToVehicle>(json);
            }

            return null;
        }

        static RechnungToBill? LoadBillJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<RechnungToBill>(json);
            }

            return null;
        }

        static List<FileInfo> FullDirList(DirectoryInfo dir, string searchPattern)
        {
            List<FileInfo> files = new List<FileInfo>();  // List that will hold the files and subfiles in path
            // Console.WriteLine("Directory {0}", dir.FullName);
            // list the files
            try
            {
                foreach (FileInfo f in dir.GetFiles(searchPattern))
                {
                    //Console.WriteLine("File {0}", f.FullName);
                    files.Add(f);
                }
            }
            catch
            {
                Console.WriteLine("Directory {0}  \n could not be accessed!!!!", dir.FullName);
                return new List<FileInfo>();  // We alredy got an error trying to access dir so dont try to access it again
            }

            // process each directory
            // If I have been able to see the files in the directory I should also be able 
            // to look at its directories so I dont think I should place this in a try catch block
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                folders.Add(d);
                files.AddRange(FullDirList(d, searchPattern));
            }

            return files;
        }
    }
}
