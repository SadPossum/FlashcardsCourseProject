using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class StorePage : ContentPage
    {
        private readonly CardSetViewModel _cardSetViewModel;
        public StorePage()
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