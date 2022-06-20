using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Converter
{
    class ImportLabViewData
    {
        static List<DirectoryInfo> folders = new List<DirectoryInfo>(); // List that hold direcotries that cannot be accessed
        

        static void Main(string[] args)
        {
            var path = "D:\\Rechnung\\übernahme kunden\\convertedRechnungsdata";
            
            var kunden = GetKundenFromPath(path);

            
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
                Console.WriteLine(customer.Name);
                var customerObject = LoadCustomerJson(customer.FullName);
                if (customerObject != null)
                {
                    kunden.Add(customerObject);
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
