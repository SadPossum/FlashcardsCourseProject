using KursahProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KursahProject.Services
{
    public class CardDataStore : IMyDataStore<Card>
    {

        private List<Card> items;

        public CardDataStore()
        {

        }
        public async Task<bool> AddItemAsync(Card item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Card item)
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

        public async Task<Card> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<Card>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    items = db.Cards.ToList();
                }
            }
            return await Task.FromResult(items);
        }
    }
}
