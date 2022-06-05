using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class AutharizationDataStore : IAutharizationDataStore
    {
        private RestAPIService _restAPIService => DependencyService.Get<RestAPIService>();

        //TODO: Проверка, если авторизация прошла не успешно
        public async Task<User> Autharization(string login, string password)
        {
            string postCardSetRequest = "/Autharization";
            User newUser = await _restAPIService.Post<User>(postCardSetRequest,
                new
                {
                    Login = login,
                    Password = password,
                });


            return await Task.FromResult(newUser);
        }
    }
}
