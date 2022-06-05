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
    [QueryProperty(nameof(ItemId), nameof(ItemId)), QueryProperty(nameof(FlashCardSetId), nameof(FlashCardSetId))]
    public class EditFlashCardViewModel : BaseViewModel
    {
        private IDataStore<FlashCard> CardDataStore => DependencyService.Get<IDataStore<FlashCard>>();

        private int? _itemId;
        private int? _flashCardSetId;
        private string _frontSideText;
        private string _frontSideImagePath;
        private string _backSideText;
        private string _backSideImagePath;
        public EditFlashCardViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(DeleteCardAsync);
            PickFrontImageCommand = new Command(PickFrontImageAsync);
            PickBackImageCommand = new Command(PickBackImageAsync);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public string FrontSideText
        {
            get => _frontSideText;
            set => SetProperty(ref _frontSideText, value);
        }

        public string FrontSideImagePath
        {
            get => _frontSideImagePath;
            set => SetProperty(ref _frontSideImagePath, value);
        }

        public string BackSideText
        {
            get => _backSideText;
            set => SetProperty(ref _backSideText, value);
        }

        public string BackSideImagePath
        {
            get => _backSideImagePath;
            set => SetProperty(ref _backSideImagePath, value);
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

        public string FlashCardSetId
        {
            set
            {
                if (int.TryParse(value, out int res))
                {
                    _flashCardSetId = res;
                }
                else
                {
                    _flashCardSetId = null;
                }
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public Command PickFrontImageCommand { get; }
        public Command PickBackImageCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            FlashCard newItem = new FlashCard
            {
                FrontSideText = FrontSideText,
                FrontSideImagePath = FrontSideImagePath,
                BackSideText = BackSideText,
                BackSideImagePath = BackSideImagePath,
                FlashCardSetId = (int)_flashCardSetId,
            };

            if (_itemId != null)
            {
                newItem.Id = (int)_itemId;
                await CardDataStore.UpdateItemAsync(newItem);
            }
            else
            {
                await CardDataStore.AddItemAsync(newItem);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public async void DeleteCardAsync()
        {
            if (_itemId != null)
            {
                await CardDataStore.DeleteItemAsync((int)_itemId);
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void PickFrontImageAsync()
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
                FrontSideImagePath = photo.FullPath;
        }

        private async void PickBackImageAsync()
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
                BackSideImagePath = photo.FullPath;
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                FlashCard item = await CardDataStore.GetItemAsync(itemId);

                FrontSideText = item.FrontSideText;
                FrontSideImagePath = item.FrontSideImagePath;
                BackSideText = item.BackSideText;
                BackSideImagePath = item.BackSideImagePath;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
