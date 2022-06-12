using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class AuthDataStore : IAuthDataStore
    {
        private RestAPIService _restAPIService => DependencyService.Get<RestAPIService>();


        //TODO: Нужна проверка на существующего пользователя.
        // Мб эта проверка происходит на стороне бэка.
        public async Task<User> Registration(string login, string password, string name)
        {

            string postCardSetRequest = "/Registration";
            User newUser = await _restAPIService.Post<User>(postCardSetRequest,
                new
                {
                    Login = login,
                    Password = password,
                    Name = name,
                });


            return await Task.FromResult(newUser);
        }

        //TODO: Проверка, если авторизация прошла не успешно. 
        // Мб эта проверка происходит на стороне бэка.
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
