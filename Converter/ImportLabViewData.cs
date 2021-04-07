using System;
using System.Collections.Generic;
using System.IO;

namespace Converter
{
    class ImportLabViewData
    {
        static List<FileInfo> files = new List<FileInfo>();  // List that will hold the files and subfiles in path
        static List<DirectoryInfo> folders = new List<DirectoryInfo>(); // List that hold direcotries that cannot be accessed

        static void Main(string[] args)
        {
            var path = "D:\\Rechnung\\übernahme kunden\\convertedRechnungsdata";


            DirectoryInfo di = new DirectoryInfo(path);
            FullDirList(di, "*");
            Console.WriteLine("Done");

            foreach (var files in files)
            {
                Console.WriteLine(files.Name);
            }

            Console.Read();

            ////if (File.Exists(path))
            ////{
            ////    File.
            ////    var data = File.ReadAllText(path);
            ////    var str = data.ToString();
            ////    Console.Write(str);
            ////}
            ////else
            ////{
            ////    Console.WriteLine("file not found!!");
            ////}

            ////Console.WriteLine("Finished");

        }

        static void FullDirList(DirectoryInfo dir, string searchPattern)
        {
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
                return;  // We alredy got an error trying to access dir so dont try to access it again
            }

            // process each directory
            // If I have been able to see the files in the directory I should also be able 
            // to look at its directories so I dont think I should place this in a try catch block
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                folders.Add(d);
                FullDirList(d, searchPattern);
            }

        }
    }
}
