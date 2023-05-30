using DocentesApp.Services;
using DocentesApp.ViewModels;

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