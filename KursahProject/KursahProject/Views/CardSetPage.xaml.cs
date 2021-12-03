using KursahProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KursahProject.Views
{
    public partial class CardSetPage : ContentPage
    {
        private CardSetViewModel _cardsetView;
        public CardSetPage()
        {
            InitializeComponent();

            BindingContext = _cardsetView = new CardSetViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _cardsetView.OnAppearing();
        }
    }
}