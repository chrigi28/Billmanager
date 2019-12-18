using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Billmanager.Database
{
    public static class SqliteDatabase
    {
        public static readonly Dictionary<Type, DbContext> Tables = new Dictionary<Type, DbContext>();

        static SqliteDatabase()
        {
            EnsureDatabases();
        }

        private static void EnsureDatabases()
        {
            Tables.Add(typeof(CustomerDbt), new GenericDbContext<CustomerDbt>("CustomerDbt"));
            Tables.Add(typeof(CarDbt), new GenericDbContext<CarDbt>("CarDbt"));
            ////Tables.Add(typeof(CustomerDbt), new GenericDbContext<ICustomerDbt>("ItemDbt"));
            ////Tables.Add(typeof(CustomerDbt), new GenericDbContext<ICustomerDbt>("ItemDbt"));
            ////Tables.Add(typeof(CustomerDbt), new GenericDbContext<ICustomerDbt>("ItemDbt"));
            ////Tables.Add(typeof(CustomerDbt), new GenericDbContext<ICustomerDbt>("ItemDbt"));
            ////Tables.Add(typeof(CustomerDbt), new GenericDbContext<ICustomerDbt>("ItemDbt"));
        }

        public static GenericDbContext<T> GetTable<T>() where T : class, IDatabaseTable
        {
            var type = typeof(T);
            if (Tables.ContainsKey(type))
            {
                var item = Tables[type];
                return (GenericDbContext<T>)item;
            }

            return null;
        }
    }
}