using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardSetDataStore : IDataStore<CardSet>
    {
        private ApplicationContext _db => DependencyService.Get<ApplicationContext>();
        public CardSetDataStore()
        {
            //_db.Database.EnsureCreated();
        }

        public async Task<bool> AddItemAsync(CardSet item)
        {
            _db.CardSet.Add(item);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CardSet item)
        {
            var oldItem = _db.CardSet.Where(a => a.Id == item.Id).FirstOrDefault();
            oldItem.Name = item.Name;
            oldItem.PictureId = item.PictureId;
            _db.CardSet.Update(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = _db.CardSet.Where(a => a.Id == id).FirstOrDefault();
            _db.CardSet.Remove(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<CardSet> GetItemAsync(int id)
        {
            return await Task.FromResult(_db.CardSet.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<CardSet>> GetItemsAsync(int? cardId = null)
        {
            return await Task.FromResult(_db.CardSet.ToList());
        }
    }
}