using KursahProject.Services;
using KursahProject.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KursahProject
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
                var db = new ApplicationContext(dbPath);
            }
            catch
            {
                new Exception("Не удалось подключиться к базе данных");
            }
            DependencyService.Register<MockDataStore>();
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
