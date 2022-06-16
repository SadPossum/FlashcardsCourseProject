using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/SadPossum/FlashcardsCourseProject"));
        }

        public ICommand OpenWebCommand { get; }
    }
}