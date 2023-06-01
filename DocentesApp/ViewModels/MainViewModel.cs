using DocentesApp.Services;
using DocentesApp.Views.Docentes;
using DocentesApp.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Command CerrarSesionCommand { get; set; }

        private DocentesView vistaDocentes = new DocentesView();
        private LoginView loginView;
        private readonly AuthService auth;
        private readonly LoginService login;

        public ContentPage Vista { get; set; }

        public MainViewModel(AuthService auth, LoginService login)
        {
            this.auth = auth;
            this.login = login;
            if (auth.IsAuthenticated && auth.IsValid)
            {
                vistaDocentes = new();
                Vista= vistaDocentes;
            }
            else
            {
                loginView = new(login);
                Vista = loginView;
            }

            PropertyChange();


            CerrarSesionCommand = new Command(CerrarSesion);
        }

        private void CerrarSesion(object obj)
        {
            login.Logout();
        }

        public void PropertyChange(string property = "") 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property)); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
