using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Billmanager.Database.Tables;
using Billmanager.Interfaces;
using Billmanager.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Billmanager.Database
{
    public static class SqliteDatabase
    {
        public static readonly Dictionary<Type, DbContext> Tables;

        static SqliteDatabase()
        {
            Tables = new Dictionary<Type, DbContext>();
            EnsureDatabases();
        }

        private static void EnsureDatabases()
        {
            Tables.Add(typeof(ItemDbt), new GenericDbContext<ItemDbt>("ItemDbt"));
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