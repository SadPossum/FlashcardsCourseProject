using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class CardSetPage : ContentPage
    {
        private readonly CardSetViewModel _cardsetView;
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