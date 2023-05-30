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

        private DocentesView vistaDocentes = new DocentesView();
        private LoginView loginView;
        private readonly AuthService auth;

        public ContentPage Vista { get; set; }

        public MainViewModel(AuthService auth, LoginService login)
        {
            this.auth = auth;

            if(auth.IsAuthenticated)
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
        }


        public void PropertyChange(string property = "") 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property)); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
