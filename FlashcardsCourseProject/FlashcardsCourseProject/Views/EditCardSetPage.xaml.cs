using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class EditCardSetPage : ContentPage
    {
        public CardSet Item { get; set; }
        public EditCardSetPage()
        {
            InitializeComponent();
            BindingContext = new EditCardSetViewModel();
        }
    }
}