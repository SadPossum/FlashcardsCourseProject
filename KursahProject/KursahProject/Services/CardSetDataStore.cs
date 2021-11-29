using KursahProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KursahProject.Services
{
    public class CardSetDataStore : IFlashCardsDataStorecs<CardSet>
    {
        private List<CardSet> items;

        public CardSetDataStore()
        {

        }

        public async Task<bool> AddItemAsync(CardSet item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CardSet item)
        {
            var oldItem = items.Where(a => a.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where(a => a.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<CardSet> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<CardSet>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    items = db.CardSets.ToList();
                }
            }
            return await Task.FromResult(items);
        }
    }
}