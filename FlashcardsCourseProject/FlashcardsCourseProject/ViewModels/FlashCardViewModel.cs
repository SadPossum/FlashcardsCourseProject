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
    public class FlashCardViewModel : BaseViewModel
    {
        private IDataStore<FlashCard> CardSetDataStore => DependencyService.Get<IDataStore<FlashCard>>();
        private FlashCard _selectedItem;
        private int? _parentId;

        public ObservableCollection<FlashCard> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<FlashCard> EditItemCommand { get; }
        public Command<FlashCard> ItemTapped { get; }

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

        public FlashCardViewModel()
        {
            Items = new ObservableCollection<FlashCard>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand((int)_parentId));

            ItemTapped = new Command<FlashCard>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            EditItemCommand = new Command<FlashCard>(OnEditItem);
        }

        async Task ExecuteLoadItemsCommand(int parentId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<FlashCard> items = await CardSetDataStore.GetItemsAsync(parentId);
                foreach (FlashCard item in items)
                {
                    FlashCard temp = new FlashCard()
                    {
                        Id = item.Id,
                        BackSideImagePath = item.BackSideImagePath,
                        BackSideText = item.BackSideText,
                        FlashCardSetId  = item.FlashCardSetId,
                        FrontSideImagePath = item.FrontSideImagePath,
                        FrontSideText = item.FrontSideText,
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

        public FlashCard SelectedItem
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
            await Shell.Current.GoToAsync($"{nameof(EditCardPage)}?{nameof(EditFlashCardViewModel.FlashCardSetId)}={_parentId}");
        }

        async void OnEditItem(FlashCard card)
        {
            if (card == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditCardPage)}?{nameof(EditFlashCardViewModel.ItemId)}={card.Id}&{nameof(EditFlashCardViewModel.FlashCardSetId)}={_parentId}");
        }

        async void OnItemSelected(FlashCard card)
        {
            if (card == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CardDetailPage)}?{nameof(FlashCardDetailViewModel.ItemId)}={card.Id}?");
        }
    }
}
