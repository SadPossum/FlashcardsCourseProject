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
            Routing.RegisterRoute(nameof(CardDetailPage), typeof(CardDetailPage));
            Routing.RegisterRoute(nameof(EditCardSetPage), typeof(EditCardSetPage));
            Routing.RegisterRoute(nameof(CardPage), typeof(CardPage));
            Routing.RegisterRoute(nameof(EditCardPage), typeof(EditCardPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
