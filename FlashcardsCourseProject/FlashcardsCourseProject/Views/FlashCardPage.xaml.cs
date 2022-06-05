using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashcardsCourseProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardPage : ContentPage
    {
        private readonly FlashCardViewModel _cardViewModel;

        public CardPage()
        {
            InitializeComponent();

            BindingContext = _cardViewModel = new FlashCardViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _cardViewModel.OnAppearing();
        }
    }
}