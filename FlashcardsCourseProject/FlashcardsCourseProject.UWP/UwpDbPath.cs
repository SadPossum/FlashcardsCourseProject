using FlashcardsCourseProject.Services;
using FlashcardsCourseProject.UWP;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpDbPath))]
namespace FlashcardsCourseProject.UWP
{
    public class UwpDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
