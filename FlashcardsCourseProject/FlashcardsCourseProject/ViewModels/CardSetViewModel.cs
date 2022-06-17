using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using FlashcardsCourseProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    public class CardSetViewModel : BaseViewModel
    {
        private CardSetDataStore CardSetDataStore => DependencyService.Get<CardSetDataStore>();
        private StoreDataStore StoreDataStore => DependencyService.Get<StoreDataStore>();

        private CardSet _selectedItem;

        public ObservableCollection<CardSet> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<CardSet> EditItemCommand { get; }
        public Command<CardSet> ItemTapped { get; }
        public Command<CardSet> PublishStoreCommand { get; }

        public CardSetViewModel()
        {
            Title = "Наборы карточек";

            Items = new ObservableCollection<CardSet>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<CardSet>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            PublishStoreCommand = new Command<CardSet>(PublishStore);
            EditItemCommand = new Command<CardSet>(OnEditItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<CardSet> items = await CardSetDataStore.GetItemsAsync();
                foreach (CardSet item in items)
                {
                    CardSet temp = new CardSet()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PicturePath = item.PicturePath,
                        PublishStore = item.PublishStore,
                    };
                    Items.Add(temp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public CardSet SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(EditCardSetPage)}");
        }

        async void OnEditItem(CardSet cardSet)
        {
            if (cardSet == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditCardSetPage)}?{nameof(EditCardSetViewModel.ItemId)}={cardSet.Id}");
        }

        async void PublishStore(CardSet cardset)
        {
            if (cardset == null)
                return;
            cardset.IsStoreCardSet = true;
            await CardSetDataStore.UpdateItemAsync(cardset);
            await StoreDataStore.AddItemAsync(cardset);
            await ExecuteLoadItemsCommand();
        }
        async void OnItemSelected(CardSet cardSet)
        {
            if (cardSet == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CardPage)}?{nameof(CardViewModel.ParentId)}={cardSet.Id}&{nameof(Title)}={cardSet.Name}");
        }
    }
}
