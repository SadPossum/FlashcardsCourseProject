using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class NewCardSetPage : ContentPage
    {
        public CardSet Item { get; set; }
        public NewCardSetPage()
        {
            InitializeComponent();
            BindingContext = new NewCardSetViewModel();
        }
    }
}