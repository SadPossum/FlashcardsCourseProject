using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class RegistrationDataStore : IRegistrationDataStore
    {
        private RestAPIService _restAPIService => DependencyService.Get<RestAPIService>();


        //TODO: Нужна проверка на существующего пользователя!
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
    }
}
