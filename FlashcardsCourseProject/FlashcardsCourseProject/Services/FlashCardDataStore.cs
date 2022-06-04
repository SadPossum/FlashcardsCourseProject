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
    public class FlashCardDataStore : IDataStore<FlashCard>
    {
        private RestAPIService _restAPIService => DependencyService.Get<RestAPIService>();

        public async Task<bool> AddItemAsync(FlashCard item)
        {
            string postCardSetRequest = "/FlashCard";
            Response<FlashCard> response = await _restAPIService.PostAdd<FlashCard>(postCardSetRequest,
               new
               {
                   FlashCardSetId = item.FlashCardSetId,
                   FrontSideText = item.FrontSideText,
                   FrontSideImagePath = item.FrontSideImagePath,
                   BackSideText = item.BackSideText,
                   BackSideImagePath = item.BackSideImagePath
               });

            return await Task.FromResult(response.Success);
        }

        public async Task<bool> UpdateItemAsync(FlashCard item)
        {
            string putCardSetRequest = $"/FlashCard/{item.Id}";
            Response<FlashCard> response = await _restAPIService.Put<FlashCard>(putCardSetRequest,
                new
                {
                    FlashCardSetId = item.FlashCardSetId,
                    FrontSideText = item.FrontSideText,
                    FrontSideImagePath = item.FrontSideImagePath,
                    BackSideText = item.BackSideText,
                    BackSideImagePath = item.BackSideImagePath

                });
            return await Task.FromResult(response.Success);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            string deleteCardSetRequest = $"/FlashCard/{id}";
            Response<FlashCard> response = await _restAPIService.Delete<FlashCard>(deleteCardSetRequest);

            return await Task.FromResult(response.Success);
        }

        public async Task<FlashCard> GetItemAsync(int id)
        {
            string getCardSetRequest = $"/FlashCard/{id}";
            return await Task.FromResult(await _restAPIService.Get<FlashCard>(getCardSetRequest, null));
        }

        public async Task<IEnumerable<FlashCard>> GetItemsAsync(int? flashCardId)
        {
            string getCardSetRequest = $"/FlashCard/Search";

            if(flashCardId != null)
            {
                return (IEnumerable<FlashCard>)GetItemAsync((int)flashCardId);
            }

            List<FlashCard> flashCardSets = await _restAPIService.Post<List<FlashCard>>(getCardSetRequest,
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
