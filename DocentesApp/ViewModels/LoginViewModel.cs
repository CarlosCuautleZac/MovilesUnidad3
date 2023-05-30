using DocentesApp.Models.DTOs;
using DocentesApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        //Comandos
        public Command IniciarSesionCommand { get; set; }

        private readonly LoginService login;
        private readonly AuthService auth;

        public LoginDTO Credenciales { get; set; } = new();
        public string Mensaje { get; set; }

        public LoginViewModel(LoginService login)
        {
            this.login = login;

            IniciarSesionCommand = new Command(IniciarSesion);
        }

        private async void IniciarSesion()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    if (!await login.IniciarSesion(Credenciales))
                    {
                        Mensaje = "Nombre de usuario o contraseña incorrectas";
                    }
                }
                else
                    Mensaje = "No hay conexion a internet";

                PropertyChange();
            }
            catch(Exception ex)
            {
                Mensaje = ex.Message;
                PropertyChange();
            }
        }

        public void PropertyChange(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
