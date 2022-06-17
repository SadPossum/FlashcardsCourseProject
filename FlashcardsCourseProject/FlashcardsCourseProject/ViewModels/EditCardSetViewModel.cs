using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditCardSetViewModel : BaseViewModel
    {
        private CardSetDataStore CardSetDataStore => DependencyService.Get<CardSetDataStore>();

        private int? _itemId;
        private string _name;
        private string _picturePath;
        private bool _isStoreCardSet;
        private bool _publishStore;
        public EditCardSetViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(DeleteCardSetAsync);
            PickImageCommand = new Command(PickImageAsync);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_name);
        }

        public bool PublishStore
        {
            get => _publishStore;
            set => SetProperty(ref _publishStore, value);
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
        public bool IsStoreCardSet
        {
            get => _isStoreCardSet;
            set => SetProperty(ref _isStoreCardSet, value);
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
        public Command DeleteCommand { get; }
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
                PicturePath = PicturePath,
                IsStoreCardSet = false
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
            if (photo != null)
                PicturePath = photo.FullPath;
            //Bitmap img = new Bitmap(photo.FullPath);
            //img.Save(Path.Combine(FileSystem.AppDataDirectory, photo.FileName), ImageFormat.Png);

        }

        public async void DeleteCardSetAsync()
        {
            if (_itemId != null)
            {
                await CardSetDataStore.DeleteItemAsync((int)_itemId);
                await Shell.Current.GoToAsync("..");
            }
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
