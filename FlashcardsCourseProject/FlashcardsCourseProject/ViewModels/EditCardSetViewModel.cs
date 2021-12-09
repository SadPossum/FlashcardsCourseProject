using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditCardSetViewModel : BaseViewModel
    {
        private IDataStore<CardSet> CardSetDataStore => DependencyService.Get<IDataStore<CardSet>>();
        private IDataStore<FileImage> FileImageDataStore => DependencyService.Get<IDataStore<FileImage>>();


        private int? _itemId;
        private string _name;
        private FileImage _picture;
        public EditCardSetViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PickImageCommand = new Command(PickImageAsync);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_name);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public FileImage Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value);
        }

        public string ItemId
        {
            set
            {
                if (int.TryParse(value, out int res))
                {
                    _itemId = res;

                    LoadItemId(res);
                }
                else
                {
                    _itemId = null;
                }
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PickImageCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            CardSet newItem = new CardSet
            {
                Name = Name,
                PictureId = Picture.Id
            };

            if (_itemId != null)
            {
                newItem.Id = (int)_itemId;
                await CardSetDataStore.UpdateItemAsync(newItem);
            }
            else
            {
                newItem.Id = 0;
                await CardSetDataStore.AddItemAsync(newItem);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void PickImageAsync()
        {
            try
            {
                FileResult photo = await MediaPicker.PickPhotoAsync();
                FileImage image = new FileImage
                {
                    Path = photo.FullPath
                };

                await FileImageDataStore.AddItemAsync(image);
                Bitmap img = new Bitmap(photo.FullPath);
                img.Save(Path.Combine(FileSystem.AppDataDirectory, photo.FileName), ImageFormat.Png);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                CardSet item = await CardSetDataStore.GetItemAsync(itemId);
                Name = item.Name;
                Picture = item.FilePicture;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
