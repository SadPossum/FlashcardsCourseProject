using System;
using Xamarin.Forms;

namespace FlashcardsCourseProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<CardSetDataStore>();
            DependencyService.Register<CardDataStore>();
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
