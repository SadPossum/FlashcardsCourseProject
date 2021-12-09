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
    [QueryProperty(nameof(ItemId), nameof(ItemId)), QueryProperty(nameof(CardSetId), nameof(CardSetId))]
    public class EditCardViewModel : BaseViewModel
    {
        private IDataStore<Card> CardDataStore => DependencyService.Get<IDataStore<Card>>();

        private int? _itemId;
        private int? _cardSetId;
        private string _frontText;
        private string _frontImagePath;
        private string _backText;
        private string _backImagePath;
        public EditCardViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PickFrontImageCommand = new Command(PickFrontImageAsync);
            PickBackImageCommand = new Command(PickBackImageAsync);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_frontText) 
                & !string.IsNullOrWhiteSpace(_backText);
        }

        public string FrontText
        {
            get => _frontText;
            set => SetProperty(ref _frontText, value);
        }

        public string FrontImagePath
        {
            get => _frontImagePath;
            set => SetProperty(ref _frontImagePath, value);
        }

        public string BackText
        {
            get => _backText;
            set => SetProperty(ref _backText, value);
        }

        public string BackImagePath
        {
            get => _backImagePath;
            set => SetProperty(ref _backImagePath, value);
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

        public string CardSetId
        {
            set
            {
                if (int.TryParse(value, out int res))
                {
                    _cardSetId = res;
                }
                else
                {
                    _cardSetId = null;
                }
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PickFrontImageCommand { get; }
        public Command PickBackImageCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Card newItem = new Card
            {
                FrontText = FrontText,
                FrontImagePath = FrontImagePath,
                BackText = BackText,
                BackImagePath = BackImagePath,
                CardSetId = (int)_cardSetId,
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

        private async void PickFrontImageAsync()
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
                FrontImagePath = photo.FullPath;
        }

        private async void PickBackImageAsync()
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
                BackImagePath = photo.FullPath;
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                Card item = await CardDataStore.GetItemAsync(itemId);
                FrontText = item.FrontText;
                FrontImagePath = item.FrontImagePath;
                BackText = item.BackText;
                BackImagePath = item.BackImagePath;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
