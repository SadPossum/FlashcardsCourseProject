using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CardSetDetailViewModel : BaseViewModel
    {
        private IDataStore<CardSet> CardSetDataStore => DependencyService.Get<IDataStore<CardSet>>();

        private int? _itemId;
        private string _name;
        private string _picturePath;

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

        public string ItemId
        {
            get
            {
                return _itemId.ToString();
            }
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

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await CardSetDataStore.GetItemAsync(itemId);
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
