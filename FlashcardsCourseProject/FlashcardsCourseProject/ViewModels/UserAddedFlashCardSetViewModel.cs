using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using FlashcardsCourseProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ParentId), nameof(ParentId))]
    public class UserAddedFlashCardSetViewModel : BaseViewModel
    {
        private IDataStore<UserAddedFlashCardSet> UserAddedFlashCardSetDataStore => DependencyService.Get<IDataStore<UserAddedFlashCardSet>>();


        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<UserAddedFlashCardSet> ItemTapped { get; }

        private UserAddedFlashCardSet _selectedItem;

        public ObservableCollection<UserAddedFlashCardSet> Items { get; }
        private int? _parentId;
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
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public UserAddedFlashCardSet SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public UserAddedFlashCardSetViewModel()
        {
            Title = "Магазин наборов карточек";

            Items = new ObservableCollection<UserAddedFlashCardSet>();

            ItemTapped = new Command<UserAddedFlashCardSet>(OnItemSelected);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand((int)_parentId));
        }

        async Task ExecuteLoadItemsCommand(int parentId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<UserAddedFlashCardSet> items = await UserAddedFlashCardSetDataStore.GetItemsAsync(parentId);
                foreach (UserAddedFlashCardSet item in items)
                {
                    UserAddedFlashCardSet temp = new UserAddedFlashCardSet()
                    {
                        UserId = item.UserId,
                        FlashCardSetId = item.FlashCardSetId
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

        async void OnItemSelected(UserAddedFlashCardSet cardSet)
        {
            if (cardSet == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CardSetPage)}?{nameof(FlashCardSetViewModel.ParentId)}={cardSet.FlashCardSetId}?");
        }
    }
}
