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

    public class FlashCardSetViewModel : BaseViewModel
    {
        private IDataStore<FlashCardSet> CardSetDataStore => DependencyService.Get<IDataStore<FlashCardSet>>();

        private FlashCardSet _selectedItem;

        public ObservableCollection<FlashCardSet> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        private int? _parentId;
        public Command<FlashCardSet> EditItemCommand { get; }
        public Command<FlashCardSet> ItemTapped { get; }

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

        public FlashCardSetViewModel()
        {
            Title = "Наборы карточек";

            Items = new ObservableCollection<FlashCardSet>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand((int)_parentId));

            ItemTapped = new Command<FlashCardSet>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            EditItemCommand = new Command<FlashCardSet>(OnEditItem);
        }

        async Task ExecuteLoadItemsCommand(int parentId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<FlashCardSet> items = await CardSetDataStore.GetItemsAsync();
                foreach (FlashCardSet item in items)
                {
                    FlashCardSet temp = new FlashCardSet()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        UserId = item.UserId,
                        ImagePath = item.ImagePath,
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

        public FlashCardSet SelectedItem
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

        async void OnEditItem(FlashCardSet cardSet)
        {
            if (cardSet == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditCardSetPage)}?{nameof(EditFlashCardSetViewModel.ItemId)}={cardSet.Id}");
        }

        async void OnItemSelected(FlashCardSet cardSet)
        {
            if (cardSet == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CardPage)}?{nameof(FlashCardViewModel.ParentId)}={cardSet.Id}&{nameof(Title)}={cardSet.Name}");
        }
    }
}
