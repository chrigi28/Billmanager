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

namespace Billmanager.Database.Tables
{
    public class GenericDbContext<T> : DbContext, IDataStore<T> where T : class, IDatabaseTable
    {
        private readonly string storagePath;
        
        public GenericDbContext(string databaseName) : base()
        {
            this.storagePath = Path.Combine(DependencyService.Get<IDbPath>().GetDbStoragePath(), databaseName + ".sqlite");
            
            Database.EnsureCreated();
        }

        public DbSet<T> Table { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = string.Format($"Filename={storagePath}");
            Debug.WriteLine($"DB-ConnectionString: {connectionString}");
            optionsBuilder.UseSqlite(connectionString);
        }

        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    if (typeof(CustomerDbt).IsAssignableFrom(typeof(T)))
        ////    {
        ////        modelBuilder.Entity<CustomerDbt>().HasKey(f => f.Id);
        ////        modelBuilder.Entity<CustomerDbt>().HasMany<CarDbt>().WithOne(c => c.Customer);
        ////        modelBuilder.Entity<CustomerDbt>().HasMany<BillDbt>().WithOne(c => c.Customer);
        ////    }
        ////    else 
        ////    if (typeof(CarDbt).IsAssignableFrom(typeof(T)))
        ////    {
        ////        modelBuilder.Entity<CarDbt>().HasKey(f => f.Id);
        ////        modelBuilder.Entity<CarDbt>().HasOne(p => p.Customer);
        ////        modelBuilder.Entity<CarDbt>().HasMany<BillDbt>().WithOne(p => p.Car);
        ////    }
        ////    else 
        ////    if (typeof(BillDbt).IsAssignableFrom(typeof(T)))
        ////    {
        ////        modelBuilder.Entity<BillDbt>().HasKey(f => f.Id);
        ////        modelBuilder.Entity<BillDbt>().HasOne(p => p.Customer);
        ////        modelBuilder.Entity<BillDbt>().HasOne(p => p.Car);
        ////        ////modelBuilder.Entity<BillDbt>().HasMany<ItemPositionDbt>().WithOne(i => i.Bill);
        ////    }
        ////}

        public async Task<bool> AddItemAsync(T item)
        {
            try
            {
                if (item.Id != -1)
                {
                    return await this.UpdateItemAsync(item);
                }

                await Table.AddAsync(item).ConfigureAwait(false);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            try
            {
                Table.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                var itemToDelete = await Table.FirstOrDefaultAsync(f => f.Id == id).ConfigureAwait(false);
                if (itemToDelete != null)
                {
                    Table.Remove(itemToDelete);
                    await SaveChangesAsync().ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<T> GetItemAsync(int id)
        {
            var row = await Table.FirstOrDefaultAsync(f => f.Id == id && !f.Deleted).ConfigureAwait(false);
            return row;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            var rows = await Table.Where(f => !f.Deleted).ToListAsync().ConfigureAwait(false);
            return rows;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> filter, bool forceRefresh = false)
        {
            var rows = await Table.Where(filter).Where(f => !f.Deleted).ToListAsync().ConfigureAwait(false);
            return rows;
        }
    }
}
