using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class CardSetPage : ContentPage
    {
        private readonly CardSetViewModel _cardSetViewModel;
        public CardSetPage()
        {
            InitializeComponent();

            BindingContext = _cardSetViewModel = new CardSetViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _cardSetViewModel.OnAppearing();
        }
    }
}