using FlashcardsCourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardDataStore : IMyDataStore<Cards>
    {

        private List<Cards> items;
        public ApplicationContext context => DependencyService.Get<ApplicationContext>();

        public CardDataStore()
        {

        }
        public async Task<bool> AddItemAsync(Cards item)
        {
            items.Add(item);
            context.Card.Add(item);
            context.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Cards item)
        {
            var oldItem = items.Where(a => a.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            context.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where(a => a.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            context.Card.Remove(oldItem);
            context.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<Cards> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<Cards>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                items = context.Card.ToList();
            }
            return await Task.FromResult(items);
        }
    }
}
