using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CardDetailViewModel : BaseViewModel
    {
        private IDataStore<Card> CardDataStore => DependencyService.Get<IDataStore<Card>>();

        public CardDetailViewModel()
        {
            ChangeSideCommand = new Command(ChangeSide);
        }

        private int? _itemId;
        private bool _frontSideVisibility = true;
        private bool _backSideVisibility = false;
        private string _frontText;
        private string _backText;
        private string _frontImagePath;
        private string _backImagePath;

        public Command ChangeSideCommand { get; }

        public bool FrontSideVisibility
        {
            get => _frontSideVisibility;
            set => SetProperty(ref _frontSideVisibility, value);
        }

        public bool BackSideVisibility
        {
            get => _backSideVisibility;
            set => SetProperty(ref _backSideVisibility, value);
        }

        public string FrontText
        {
            get => _frontText;
            set => SetProperty(ref _frontText, value);
        }

        public string BackText
        {
            get => _backText;
            set => SetProperty(ref _backText, value);
        }

        public string FrontImagePath
        {
            get => _frontImagePath;
            set => SetProperty(ref _frontImagePath, value);
        }

        public string BackImagePath
        {
            get => _backImagePath;
            set => SetProperty(ref _backImagePath, value);
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
                var item = await CardDataStore.GetItemAsync(itemId);
                FrontText = item.FrontText;
                BackText = item.BackText;
                FrontImagePath = item.FrontImagePath;
                BackImagePath = item.BackImagePath;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private void ChangeSide()
        {
            FrontSideVisibility = !FrontSideVisibility;
            BackSideVisibility = !BackSideVisibility;
        }
    }
}
