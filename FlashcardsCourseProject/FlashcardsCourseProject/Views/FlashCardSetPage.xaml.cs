using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class CardSetPage : ContentPage
    {
        private readonly FlashCardSetViewModel _cardSetViewModel;
        public CardSetPage()
        {
            InitializeComponent();

            BindingContext = _cardSetViewModel = new FlashCardSetViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _cardSetViewModel.OnAppearing();
        }
    }
}