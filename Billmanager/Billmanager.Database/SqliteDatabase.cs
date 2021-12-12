using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Billmanager.Database;

public static class SqliteDatabase
{
    ////public static readonly Dictionary<Type, DbContext> Tables = new Dictionary<Type, DbContext>();
    private static Repository repo;

    static SqliteDatabase()
    {
        repo = new Repository();
    }

    public static Repository AssureDb()
    {
        if (repo != null)
        {
            return repo;
        }

        return repo = new Repository();
    }
}