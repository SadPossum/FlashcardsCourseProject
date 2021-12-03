using FlashcardsCourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardDataStore : IDataStore<Card>
    {

        private List<Card> items;
        public ApplicationContext context => DependencyService.Get<ApplicationContext>();

        public CardDataStore()
        {

        }
        public async Task<bool> AddItemAsync(Card item)
        {
            items.Add(item);
            context.Card.Add(item);
            context.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Card item)
        {
            var oldItem = items.Where(a => a.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            context.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where(a => a.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            context.Card.Remove(oldItem);
            context.SaveChanges();

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
                items = context.Card.ToList();
            }
            return await Task.FromResult(items);
        }
    }
}
