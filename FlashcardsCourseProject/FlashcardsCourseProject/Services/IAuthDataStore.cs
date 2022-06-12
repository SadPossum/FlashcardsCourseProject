using FlashcardsCourseProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsCourseProject.Services
{
    public interface IAuthDataStore
    {
        Task<User> Registration(string login, string password, string name);
        Task<User> Autharization(string login, string password);
    }
}
