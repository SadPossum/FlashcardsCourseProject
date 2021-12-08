using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardDataStore : IDataStore<Card>
    {
        private ApplicationContext _db => DependencyService.Get<ApplicationContext>();

        public CardDataStore()
        {
            _db.Database.EnsureCreated();
        }
        public async Task<bool> AddItemAsync(Card item)
        {
            _db.Card.Add(item);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Card item)
        {
            var oldItem = _db.Card.Where(a => a.Id == item.Id).FirstOrDefault();
            oldItem.FrontText = item.FrontText;
            oldItem.BackImageId = item.BackImageId;
            oldItem.BackText = item.BackText;
            oldItem.FrontImageId = item.FrontImageId;
            _db.Card.Update(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = _db.Card.Where(a => a.Id == id).FirstOrDefault();
            _db.Card.Remove(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<Card> GetItemAsync(int id)
        {
            return await Task.FromResult(_db.Card.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<Card>> GetItemsAsync(int? cardSetId)
        {
            return await Task.FromResult(_db.Card.ToList());
        }
    }
}
