using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class CardDetailPage : ContentPage
    {
        public CardDetailPage()
        {
            InitializeComponent();
            BindingContext = new FlashCardDetailViewModel();
        }
    }
}