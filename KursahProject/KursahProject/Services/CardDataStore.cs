using KursahProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KursahProject.Services
{
    public class CardDataStore : IDataStore<Card>
    {

        private List<Card> items;
        public ApplicationContext db => DependencyService.Get<ApplicationContext>();

        public CardDataStore()
        {
            db.Database.EnsureCreated();
            //if (db.Card.Count() == 0)
            //{
            //    db.Card.Add(new Card { Id = Guid.NewGuid().ToString(), Name = "Карточка №1", FrontImage = "frontImagePath", BackImage= "backImagePath", BackText = "Карточка №1", new CardSet { Id = } });
            //    db.SaveChanges();
            //}
        }
        public async Task<bool> AddItemAsync(Card item)
        {
            items.Add(item);
            db.Card.Add(item);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Card item)
        {
            var oldItem = items.Where(a => a.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            db.Card.Update(item);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where(a => a.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            db.Card.Remove(oldItem);
            db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<Card> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<Card>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                items = db.Card.ToList();
            }
            return await Task.FromResult(items);
        }
    }
}
