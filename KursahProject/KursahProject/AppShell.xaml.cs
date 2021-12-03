using KursahProject.ViewModels;
using KursahProject.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KursahProject
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
