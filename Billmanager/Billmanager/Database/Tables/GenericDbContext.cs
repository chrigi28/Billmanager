using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Billmanager.Interfaces;
using Billmanager.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Billmanager.Database.Tables
{
    public class GenericDbContext<T> : DbContext, IDataStore<T> where T : class, IDatabaseTable
    {
        private readonly string storagePath;

        public GenericDbContext(string databaseName)
        {
            this.storagePath = Path.Combine(DependencyService.Get<IDbPath>().GetDbStoragePath(), databaseName);

            Database.EnsureCreated();
        }

        public DbSet<T> Table { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={storagePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // todo set necessery columns depending on the type
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<T>().HasKey(f => f.Id);
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
