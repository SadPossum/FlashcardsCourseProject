using FlashcardsCourseProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsCourseProject.Services
{
    public interface IAutharizationDataStore
    {
        Task<User> Autharization(string login, string password);
    }
}
