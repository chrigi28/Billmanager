using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Billmanager.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Billmanager.Database.Tables;

public class Repository : DbContext, IDataStore
{
    private readonly string storagePath;
        
    public Repository() : base()
    {
        var path = DependencyService.Get<IDbPath>().GetDbStoragePath();
        this.storagePath = Path.Combine(path, "Billmanager.sqlite");

        Directory.CreateDirectory(path);

        Database.EnsureCreated();
        //Database.Migrate();
    }    

    public DbSet<CustomerDbt> Customer { get; set; }
    public DbSet<CarDbt> Car { get; set; }
    public DbSet<BillDbt> Bill { get; set; }
    public DbSet<ItemPositionDbt> ItemPosition { get; set; }
    public DbSet<SettingsDbt> Setting { get; set; }
    ////public DbSet<CustomerDbt> Customer { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = string.Format($"Filename={storagePath}");
        Debug.WriteLine($"DB-ConnectionString: {connectionString}");
        optionsBuilder.UseSqlite(connectionString).UseLazyLoadingProxies();;
        base.OnConfiguring(optionsBuilder);
            
    }
        
    public async Task<bool> AddItemAsync<T>(T item) where T : class, IDatabaseTable
    {
        try
        {
            await this.AddAsync(item);
            await this.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateItemAsync<T>(T item) where T : class, IDatabaseTable
    {
        try
        {
            this.Update(item);
            await this.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteItemAsync<T>(int id) where T : class, IDatabaseTable
    {
        try
        {
            var itemToDelete = await this.FindAsync<T>(id).ConfigureAwait(false);
            if (itemToDelete != null)
            {
                this.Remove(itemToDelete);
                await this.SaveChangesAsync().ConfigureAwait(false);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<T> GetItemAsync<T>(int id) where T : class, IDatabaseTable
    {
        var row = await this.Set<T>().SingleOrDefaultAsync(f => f.Id == id && !f.Deleted).ConfigureAwait(false);
        return row;
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(bool forceRefresh = false) where T : class, IDatabaseTable
    {
        var rows = await this.Set<T>().Where(f => !f.Deleted).ToListAsync();
        return rows;
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> filter, bool forceRefresh = false)
        where T : class, IDatabaseTable
    {
        var rows = await this.Set<T>().Where(filter).Where(f => !f.Deleted).ToListAsync().ConfigureAwait(false);
        return rows;
    }
}