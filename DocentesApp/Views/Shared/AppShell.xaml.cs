using DocentesApp.Services;
using DocentesApp.ViewModels;
using DocentesApp.Views.Docentes;

namespace DocentesApp
{
    public partial class AppShell : Shell
    {

        public AppShell(AuthService auth, LoginService login)
        {
           

            this.BindingContext = new MainViewModel(auth, login);

            InitializeComponent();
        }
    }
}