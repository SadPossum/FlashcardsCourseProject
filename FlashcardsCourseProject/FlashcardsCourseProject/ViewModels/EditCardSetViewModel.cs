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

        private int? _itemId;
        private string _name;
        private string _picturePath;
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

        public string PicturePath
        {
            get => _picturePath;
            set => SetProperty(ref _picturePath, value);
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
                PicturePath = PicturePath
            };

            if (_itemId != null)
            {
                newItem.Id = (int)_itemId;
                await CardSetDataStore.UpdateItemAsync(newItem);
            }
            else
            {
                //newItem.Id = 0;
                await CardSetDataStore.AddItemAsync(newItem);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void PickImageAsync()
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            _picturePath = photo.FullPath;
            //Bitmap img = new Bitmap(photo.FullPath);
            //img.Save(Path.Combine(FileSystem.AppDataDirectory, photo.FileName), ImageFormat.Png);

        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                CardSet item = await CardSetDataStore.GetItemAsync(itemId);
                Name = item.Name;
                PicturePath = item.PicturePath;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
