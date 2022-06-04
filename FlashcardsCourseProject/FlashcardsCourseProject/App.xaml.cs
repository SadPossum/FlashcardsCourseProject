using FlashcardsCourseProject.Models;
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
