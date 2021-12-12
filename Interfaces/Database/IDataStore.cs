using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Billmanager.Interfaces.Database;

public interface IDataStore
{
    //Task<bool> AddItemAsync<T>(T item);
    //Task<bool> UpdateItemAsync<T>(T item);
    //Task<bool> DeleteItemAsync<T>(int id);
    //Task<T> GetItemAsync<T>(int id);
    //Task<IEnumerable<T>> GetItemsAsync<T>(bool forceRefresh = false);
    Task<bool> AddItemAsync<T>(T item) where T : class, IDatabaseTable;
    Task<bool> UpdateItemAsync<T>(T item) where T : class, IDatabaseTable;
    Task<bool> DeleteItemAsync<T>(int id) where T : class, IDatabaseTable;
    Task<T> GetItemAsync<T>(int id)  where T : class, IDatabaseTable;
    Task<IEnumerable<T>> GetItemsAsync<T>(bool forceRefresh = false)  where T : class, IDatabaseTable;
    Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> filter, bool forceRefresh = false) where T : class, IDatabaseTable;
}