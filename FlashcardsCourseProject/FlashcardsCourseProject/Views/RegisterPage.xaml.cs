using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashcardsCourseProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        private string _secondPassword;
        public string SecondPassword
        {
            get { return _secondPassword; }
            set { _secondPassword = value; OnPropertyChanged(); }
        }
        private string _allert;
        public string Allert
        {
            get { return _allert; }
            set { _allert = value; OnPropertyChanged(); }
        }
        private bool _isShowAllert;
        public bool IsShowAllert
        {
            get { return _isShowAllert; }
            set { _isShowAllert = value; OnPropertyChanged(); }
        }
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        private bool CheckCorrectData()
        {
            bool result = true;
            try
            {
                if (Username == null && Password == null && SecondPassword == null)
                    result = false;
                if (Password != SecondPassword)
                    result = false;
                //if (_username[0].ToString() == " " ||
                //    _password[0].ToString() == " " ||
                //    _secondPassword[0].ToString() == " ")
                //    result = false; 
            }
            catch { };
            
            return result;
        }
        private void ButtonConfirm_Clicked(object sender, EventArgs e)
        {
            if (CheckCorrectData())
                Application.Current.MainPage = new LoginPage(_username, _password);
            else
            {
                IsShowAllert = true;
                Allert = "Некорректно введены данные";
            }
        }
        private void ButtonBackToLogin_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}