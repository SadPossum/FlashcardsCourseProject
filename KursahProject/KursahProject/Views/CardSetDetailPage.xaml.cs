using KursahProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KursahProject.Views
{
    public partial class CardSetDetailPage : ContentPage
    {
        public CardSetDetailPage()
        {
            InitializeComponent();
            BindingContext = new CardSetDetailViewModel();
        }
    }
}