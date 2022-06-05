using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    public class AutharizationViewModel : BaseViewModel
    {
        private IAutharizationDataStore AutharizationDataStore => DependencyService.Get<IAutharizationDataStore>();

        //private string _name;
        private string _login;
        private string _password;
        public Command AutharizationCommand { get; }


        //public string Name
        //{
        //    get => _name;
        //    set => SetProperty(ref _name, value);
        //}

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public AutharizationViewModel()
        {
            AutharizationCommand = new Command(async () => await AutharizationUserCommand());

        }

        async Task AutharizationUserCommand()
        {
            IsBusy = true;
            try
            {
                IsBusy = true;
                AuthUser = await AutharizationDataStore.Autharization(Login, Password);

                //TODO: Если авторизацяи не удалась, вывести какой ни будь меседж
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
