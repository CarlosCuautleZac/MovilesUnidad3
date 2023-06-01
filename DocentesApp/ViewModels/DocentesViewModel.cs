using DocentesApp.Models.DTOs;
using DocentesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.ViewModels
{
    public class DocentesViewModel : INotifyPropertyChanged
    {
        private readonly AuthService auth;
        private readonly DocentesService docentesService;

        public ObservableCollection<Docente> Docentes { get; set; } = new();
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public DocentesViewModel(AuthService auth, DocentesService docentesService)
        {
            this.auth = auth;
            this.docentesService = docentesService;

            var claims = auth.Cliams;
            //Esto se pone cuando sabes que si existe
            Nombre = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            //Esto se pone si encuentra alguno
            Correo = claims.Where(x => x.Type == ClaimTypes.Email).Select(x => x.Value).FirstOrDefault();

            Cargar();

            PropertyChange();
        }

        private async void Cargar()
        {
            Docentes.Clear();
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var lista = await docentesService.Get();
                
                lista.ForEach(x => Docentes.Add(x));
            }
        }

        public void PropertyChange(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
