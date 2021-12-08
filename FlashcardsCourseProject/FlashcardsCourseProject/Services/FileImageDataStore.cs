using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class FileImageDataStore : IDataStore<FileImage>
    {
        private ApplicationContext _db => DependencyService.Get<ApplicationContext>();
        public FileImageDataStore()
        {
            _db.Database.EnsureCreated();
        }

        public async Task<bool> AddItemAsync(FileImage item)
        {
            _db.FileImage.Add(item);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(FileImage item)
        {
            var oldItem = _db.FileImage.Where(a => a.Id == item.Id).FirstOrDefault();
            oldItem.Name = item.Name;
            oldItem.Path = item.Path;
            _db.FileImage.Update(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = _db.FileImage.Where(a => a.Id == id).FirstOrDefault();
            _db.FileImage.Remove(oldItem);
            _db.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<FileImage> GetItemAsync(int id)
        {
            return await Task.FromResult(_db.FileImage.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<FileImage>> GetItemsAsync(int? cardId = null)
        {
            return await Task.FromResult(_db.FileImage.ToList());
        }
    }
}
