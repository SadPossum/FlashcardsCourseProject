using FlashcardsCourseProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    public class AutharizationViewModel
    {
        public Command AutharizationCommand { get; }

        public AutharizationViewModel()
        {
            AutharizationCommand = new Command(Autharization);
        }
        private async void Autharization(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
