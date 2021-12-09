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
    [QueryProperty(nameof(ParentId), nameof(ParentId))]
    public class CardViewModel : BaseViewModel
    {
        private IDataStore<Card> CardSetDataStore => DependencyService.Get<IDataStore<Card>>();
        private Card _selectedItem;
        private int? _parentId;

        public ObservableCollection<Card> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Card> EditItemCommand { get; }
        public Command<Card> ItemTapped { get; }

        public string ParentId
        {
            set
            {
                if (int.TryParse(value, out int res))
                {
                    _parentId = res;

                    _ = ExecuteLoadItemsCommand(res);
                }
                else
                {
                    _parentId = null;
                }
            }
        }

        public CardViewModel()
        {
            Items = new ObservableCollection<Card>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand((int)_parentId));

            ItemTapped = new Command<Card>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            EditItemCommand = new Command<Card>(OnEditItem);
        }

        async Task ExecuteLoadItemsCommand(int parentId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<Card> items = await CardSetDataStore.GetItemsAsync(parentId);
                foreach (Card item in items)
                {
                    Card temp = new Card()
                    {
                        Id = item.Id,
                        FrontText = item.FrontText,
                        FrontImagePath = item.FrontImagePath,
                        BackText = item.BackText,
                        BackImagePath = item.BackImagePath,
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

        public Card SelectedItem
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
            await Shell.Current.GoToAsync($"{nameof(EditCardPage)}?{nameof(EditCardViewModel.CardSetId)}={_parentId}");
        }

        async void OnEditItem(Card card)
        {
            if (card == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditCardPage)}?{nameof(EditCardViewModel.ItemId)}={card.Id}&{nameof(EditCardViewModel.CardSetId)}={_parentId}");
        }

        async void OnItemSelected(Card card)
        {
            if (card == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CardDetailPage)}?{nameof(CardDetailViewModel.ItemId)}={card.Id}?");
        }
    }
}
