using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using FlashcardsCourseProject.Services.API;
using System;
using Xamarin.Forms;

namespace FlashcardsCourseProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<FlashCardSetDataStore>();
            DependencyService.Register<FlashCardDataStore>();
            DependencyService.Register<AuthService>();
            DependencyService.Register<RestAPIService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
