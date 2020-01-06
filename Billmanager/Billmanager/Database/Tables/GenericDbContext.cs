using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (typeof(CustomerDbt).IsAssignableFrom(typeof(T)))
            {
                modelBuilder.Entity<CustomerDbt>().HasKey(f => f.CustomerId);
                modelBuilder.Entity<CustomerDbt>()
                    .HasMany<CarDbt>()
                    .WithOne(c => c.Customer);
            }
            else 
            if (typeof(CarDbt).IsAssignableFrom(typeof(T)))
            {
                modelBuilder.Entity<CarDbt>().HasKey(f => f.CarId);
                modelBuilder.Entity<CarDbt>()
                    .HasOne(p => p.Customer);
            }
            else 
            if (typeof(BillDbt).IsAssignableFrom(typeof(T)))
            {
                modelBuilder.Entity<BillDbt>().HasKey(f => f.BillId);
                modelBuilder.Entity<BillDbt>().HasOne(p => p.Customer);
                ////modelBuilder.Entity<BillDbt>().HasMany<ItemPositionDbt>().WithOne(i => i.Bill);
            }
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
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

        public async Task<bool> DeleteItemAsync(string id)
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

        public async Task<T> GetItemAsync(string id)
        {
            var row = await Table.FirstOrDefaultAsync(f => f.Id == id).ConfigureAwait(false);
            return row;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            var rows = await Table.ToListAsync().ConfigureAwait(false);
            return rows;
        }
    }
}
