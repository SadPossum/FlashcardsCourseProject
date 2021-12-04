using FlashcardsCourseProject.Views;
using System;
using Xamarin.Forms;

namespace FlashcardsCourseProject
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CardSetDetailPage), typeof(CardSetDetailPage));
            Routing.RegisterRoute(nameof(NewCardSetPage), typeof(NewCardSetPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
