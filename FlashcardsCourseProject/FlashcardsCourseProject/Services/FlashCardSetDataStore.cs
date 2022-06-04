using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services.API;
using FlashcardsCourseProject.Services.API.Models;
using FlashcardsCourseProject.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class FlashCardSetDataStore : IDataStore<FlashCardSet>
    {
        private RestAPIService _restAPIService => DependencyService.Get<RestAPIService>();

        public async Task<bool> AddItemAsync(FlashCardSet item)
        {
            string postCardSetRequest = "/FlashCardSet";
            Response<FlashCardSet> response = await _restAPIService.PostAdd<FlashCardSet>(postCardSetRequest,
               new
               {
                   Name = item.Name,
                   Description = item.Description,
                   ImagePath = item.ImagePath
               });

            return await Task.FromResult(response.Success);
        }

        public async Task<bool> UpdateItemAsync(FlashCardSet item)
        {
            string putCardSetRequest = $"/FlashCardSet/{item.Id}";

            Response<FlashCardSet> response = await _restAPIService.Put<FlashCardSet>(putCardSetRequest,
                new
                {
                    Name = item.Name,
                    Description = item.Description,
                    ImagePath = item.ImagePath
                });

            return await Task.FromResult(response.Success);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            string deleteCardSetRequest = $"/FlashCardSet/{id}";

            Response<FlashCardSet> response = await _restAPIService.Delete<FlashCardSet>(deleteCardSetRequest);

            return await Task.FromResult(response.Success);
        }

        public async Task<FlashCardSet> GetItemAsync(int id)
        {
            string getCardSetRequest = $"/FlashCardSet/{id}";
            return await Task.FromResult(await _restAPIService.Get<FlashCardSet>(getCardSetRequest, null));
        }

        public async Task<IEnumerable<FlashCardSet>> GetItemsAsync(int? cardSetId = null)
        {
            string getCardSetRequest = $"/FlashCardSet/Search";

            if(cardSetId != null)
            {
                return (IEnumerable<FlashCardSet>)GetItemAsync((int)cardSetId);
            }
            List<FlashCardSet> flashCardSets = await _restAPIService.Post<List<FlashCardSet>>(getCardSetRequest,
                new ListRequest
                {
                    Search = "smth",
                    Pagination = new Pagination
                    {
                        Page = 1,
                        PageSize = 10,
                    }
                });
            return await Task.FromResult(flashCardSets);
        }
    }
}