using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class StoreDataStore : IDataStore<CardSet>
    {
        private ApplicationContext _db => DependencyService.Get<ApplicationContext>();
        public StoreDataStore()
        {
            _db.Database.EnsureCreated();

        }

        public async Task<bool> AddItemAsync(CardSet item)
        {

            var query = _db.Store.Where(x => x.CardSetId == item.Id).FirstOrDefault();

            if(query == null)
            {
                Store store = new Store() {  CardSetId = item.Id };
                _db.Store.Add(store);
                _db.SaveChanges();
            }
            

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CardSet item)
        {
            var oldItem = _db.CardSet.Where(a => a.Id == item.Id).FirstOrDefault();
            oldItem.Name = item.Name;
            oldItem.PicturePath = item.PicturePath;
            oldItem.IsStoreCardSet = false;
            oldItem.IsDelete = false;
            oldItem.PublishStore = false;
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
            //return await Task.FromResult(_db.Store.FirstOrDefault(a => a.Id == id));
            return await Task.FromResult(_db.CardSet.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<CardSet>> GetItemsAsync(int? cardId = null)
        {
            var query = (from a in _db.Store
                         join b in _db.CardSet
                         on a.CardSetId equals b.Id
                         select b).ToList();

            return await Task.FromResult(query);
        }
    }
}