using DocentesApp.Services;

namespace DocentesApp
{
    public partial class App : Application
    {
        public App(AuthService auth, LoginService login)
        {
            InitializeComponent();

            MainPage = new AppShell(auth, login);
        }
    }
}