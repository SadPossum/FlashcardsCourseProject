using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardSetDataStore : IDataStore<CardSet>
    {
        private List<CardSet> items;
        public ApplicationContext db => DependencyService.Get<ApplicationContext>();
        public CardSetDataStore()
        {
            db.Database.EnsureCreated();
        }

        public async Task<bool> AddItemAsync(CardSet item)
        {
            items.Add(item);
            db.CardSet.Add(item);
            db.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CardSet item)
        {
            var oldItem = items.Where(a => a.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            db.CardSet.Update(item);
            db.SaveChanges();


            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where(a => a.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            db.CardSet.Remove(oldItem);
            db.SaveChanges();


            return await Task.FromResult(true);
        }

        public async Task<CardSet> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<CardSet>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                items = db.CardSet.ToList();
            }
            return await Task.FromResult(items);
        }
    }
}