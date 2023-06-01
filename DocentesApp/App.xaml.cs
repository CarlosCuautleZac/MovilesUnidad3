
using DocentesApp.Services;
using DocentesApp.Views.Docentes;
using DocentesApp.Views.Login;

namespace DocentesApp
{
    public partial class App : Application
    {
        public App(AuthService auth, LoginService login)
        {
            Routing.RegisterRoute("main", typeof(DocentesView));
            Routing.RegisterRoute("login", typeof(LoginView));
            InitializeComponent();

            MainPage = new AppShell(auth, login);
        }
    }
}