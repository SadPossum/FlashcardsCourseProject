using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class CardSetDetailPage : ContentPage
    {
        public CardSetDetailPage()
        {
            InitializeComponent();
            BindingContext = new CardSetDetailViewModel();
        }
    }
}