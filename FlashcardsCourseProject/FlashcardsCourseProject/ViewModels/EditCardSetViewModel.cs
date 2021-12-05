using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditCardSetViewModel : BaseViewModel
    {
        private IDataStore<CardSet> CardSetDataStore => DependencyService.Get<IDataStore<CardSet>>();

        private string _itemId;
        private string _name;
        private string _picture;
        public EditCardSetViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_name)
                && !string.IsNullOrWhiteSpace(_picture);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value);
        }

        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;

                if (_itemId != null)
                {
                    LoadItemId(value);
                }
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

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
                Picture = Picture
            };

            if (ItemId != null)
            {
                newItem.Id = ItemId;
                await CardSetDataStore.UpdateItemAsync(newItem);
            }
            else
            {
                newItem.Id = Guid.NewGuid().ToString();
                await CardSetDataStore.AddItemAsync(newItem);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await CardSetDataStore.GetItemAsync(itemId);
                Name = item.Name;
                Picture = item.Picture;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
