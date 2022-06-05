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
    public class UserAddedFlashCardSetDataStore : IDataStore<UserAddedFlashCardSet>
    {
        private RestAPIService _restAPIService => DependencyService.Get<RestAPIService>();

        public async Task<bool> AddItemAsync(UserAddedFlashCardSet item)
        {
            string postCardSetRequest = "/UserAddedFlashCardSet";
            Response<UserAddedFlashCardSet> response = await _restAPIService.PostAdd<UserAddedFlashCardSet>(postCardSetRequest,
               new
               {
                   FlashCardSetId = item.FlashCardSetId,
                   UserId = item.UserId
               });

            return await Task.FromResult(response.Success);
        }

        public async Task<bool> UpdateItemAsync(UserAddedFlashCardSet item)
        {
            //string putCardSetRequest = $"/UserAddedFlashCardSet/{item.Id}";
            //Response<UserAddedFlashCardSet> response = await _restAPIService.Put<UserAddedFlashCardSet>(putCardSetRequest,
            //    new
            //    {
            //        FlashCardSetId = item.FlashCardSetId,
            //        FrontSideText = item.FrontSideText,
            //        FrontSideImagePath = item.FrontSideImagePath,
            //        BackSideText = item.BackSideText,
            //        BackSideImagePath = item.BackSideImagePath

            //    });
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            string deleteCardSetRequest = $"/UserAddedFlashCardSet/{id}";
            Response<UserAddedFlashCardSet> response = await _restAPIService.Delete<UserAddedFlashCardSet>(deleteCardSetRequest);

            return await Task.FromResult(response.Success);
        }

        public async Task<UserAddedFlashCardSet> GetItemAsync(int id)
        {
            string getCardSetRequest = $"/UserAddedFlashCardSet/{id}";
            return await Task.FromResult(await _restAPIService.Get<UserAddedFlashCardSet>(getCardSetRequest, null));
        }

        public async Task<IEnumerable<UserAddedFlashCardSet>> GetItemsAsync(int? id)
        {
            string getCardSetRequest = $"/UserAddedFlashCardSet/Search";

            if(id != null)
            {
                return (IEnumerable<UserAddedFlashCardSet>)GetItemAsync((int)id);
            }

            List<UserAddedFlashCardSet> flashCardSets = await _restAPIService.Post<List<UserAddedFlashCardSet>>(getCardSetRequest,
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
