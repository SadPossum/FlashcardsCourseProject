using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashcardsCourseProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private string innerLogin;
        private string innerPassword;
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
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        public LoginPage(string login, string password)
        {
            InitializeComponent();
            this.BindingContext = this;
            innerLogin = login;
            innerPassword = password;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if(Username == "Admin" && Password == "admin") 
                Application.Current.MainPage = new AppShell();
            else if (Username == innerLogin && Password == innerPassword
                    && Username != null && Password != null)
                Application.Current.MainPage = new AppShell();
            else
            {
                if(Username != null && Password != null)
                    Allert = "Заполните корректно все поля";
                Allert = "Введен неверный логин / пароль";
                IsShowAllert = true;
            }    
            
        }
        private void ButtonRegister_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new RegisterPage();

        }
    }
}