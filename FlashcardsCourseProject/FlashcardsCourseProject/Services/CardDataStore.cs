using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardDataStore : IDataStore<Card>
    {

        private List<Card> _items;
        private ApplicationContext _db => DependencyService.Get<ApplicationContext>();

        public CardDataStore()
        {
            _db.Database.EnsureCreated();
        }
        public async Task<bool> AddItemAsync(Card item)
        {
            _items.Add(item);
            _db.Card.Add(item);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Card item)
        {
            var oldItem = _items.Where(a => a.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);
            _db.Card.Update(item);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.Where(a => a.Id == id).FirstOrDefault();
            _items.Remove(oldItem);
            _db.Card.Remove(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<Card> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<Card>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                _items = _db.Card.ToList();
            }
            return await Task.FromResult(_items);
        }
    }
}
