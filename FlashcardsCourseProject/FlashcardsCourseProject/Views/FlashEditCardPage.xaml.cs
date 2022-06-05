using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.ViewModels;
using System;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class EditCardPage : ContentPage
    {
        public FlashCard Item { get; set; }
        public EditCardPage()
        {
            InitializeComponent();
            BindingContext = new EditFlashCardViewModel();
        }
    }
}