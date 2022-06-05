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
    public class RegistrationViewModel : BaseViewModel
    {
        private IRegistrationDataStore RegistrationDataStore => DependencyService.Get<IRegistrationDataStore>();

        private string _name;
        private string _login;
        private string _password;
        public Command RegistrationCommand { get; }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

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

        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(async () => await RegistrationUserCommand());

        }

        async Task RegistrationUserCommand()
        {
            IsBusy = true;
            try
            {
                IsBusy = true;
                AuthUser = await RegistrationDataStore.Registration(Login, Password, Name);
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
