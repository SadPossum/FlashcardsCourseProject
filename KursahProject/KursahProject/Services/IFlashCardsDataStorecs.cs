using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KursahProject.Services
{
    public interface IFlashCardsDataStorecs<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
