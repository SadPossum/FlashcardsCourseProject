using KursahProject.Models;
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
    public partial class NewCardSetPage : ContentPage
    {
        public CardSet Item { get; set; }
        public NewCardSetPage()
        {
            InitializeComponent();
            BindingContext = new NewCardSetViewModel();
        }
    }
}