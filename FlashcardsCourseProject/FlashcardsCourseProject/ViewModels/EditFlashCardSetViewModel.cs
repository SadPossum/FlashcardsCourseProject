using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditFlashCardSetViewModel : BaseViewModel
    {
        private IDataStore<FlashCardSet> CardSetDataStore => DependencyService.Get<IDataStore<FlashCardSet>>();

        private int? _itemId;
        private string _name;
        private string _imagePath;
        private int _userId;
        private string _description;
        public EditFlashCardSetViewModel()
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

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
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
            FlashCardSet newItem = new FlashCardSet
            {
                Name = Name,
                Description = Description,
                ImagePath = ImagePath,
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
                ImagePath = photo.FullPath;
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
                FlashCardSet item = await CardSetDataStore.GetItemAsync(itemId);
                Name = item.Name;
                Description = item.Description;
                ImagePath = item.ImagePath;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
