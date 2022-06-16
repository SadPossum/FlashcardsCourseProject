using FlashcardsCourseProject.Services;
using System;
using Xamarin.Forms;

namespace FlashcardsCourseProject
{
    public partial class App : Application
    {
        public const string DBFILENAME = "AppData.db";
        public App()
        {
            InitializeComponent();
            try
            {
                string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
                DependencyService.RegisterSingleton(new ApplicationContext(dbPath));
            }
            catch
            {
                new Exception("Не удалось подключиться к базе данных");
            }

            DependencyService.Register<StoreDataStore>();
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
