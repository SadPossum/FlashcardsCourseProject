using FlashcardsCourseProject.Models;
using FlashcardsCourseProject.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FlashcardsCourseProject.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class FlashCardDetailViewModel : BaseViewModel
    {
        private IDataStore<FlashCard> CardDataStore => DependencyService.Get<IDataStore<FlashCard>>();

        public FlashCardDetailViewModel()
        {
            ChangeSideCommand = new Command(ChangeSide);
        }

        private int? _itemId;
        private bool _frontSideVisibility = true;
        private bool _backSideVisibility = false;
        private string _frontSideText;
        private string _backSideText;
        private string _frontSideImagePath;
        private string _backSideImagePath;

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
                FrontSideText = item.FrontSideText;
                BackSideText = item.BackSideText;
                FrontSideImagePath = item.FrontSideImagePath;
                BackSideImagePath = item.BackSideImagePath;
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
