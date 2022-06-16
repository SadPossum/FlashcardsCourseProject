using FlashcardsCourseProject.ViewModels;

using Xamarin.Forms;

namespace FlashcardsCourseProject.Views
{
    public partial class StorePage : ContentPage
    {
        private readonly StoreViewModel _storeViewModel;
        public StorePage()
        {
            InitializeComponent();

            BindingContext = _storeViewModel = new StoreViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _storeViewModel.OnAppearing();
        }
    }
}