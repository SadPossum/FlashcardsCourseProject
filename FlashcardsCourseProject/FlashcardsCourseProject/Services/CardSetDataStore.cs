using FlashcardsCourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardSetDataStore : IMyDataStore<CardSets>
    {
        private List<CardSets> items;
        public ApplicationContext context => DependencyService.Get<ApplicationContext>();
        public CardSetDataStore()
        {

        }

        public async Task<bool> AddItemAsync(CardSets item)
        {
            items.Add(item);
            context.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CardSets item)
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
            context.SaveChanges();


            return await Task.FromResult(true);
        }

        public async Task<CardSets> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<CardSets>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                items = context.CardSet.ToList();
            }
            return await Task.FromResult(items);
        }
    }
}