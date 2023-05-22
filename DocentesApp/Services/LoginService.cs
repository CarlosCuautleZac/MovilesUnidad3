using DocentesApp.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.Services
{
    public class LoginService
    {
        public string url = "https://docentes.itesrc.net/";
        HttpClient client;
        public LoginService()
        {
            //Le paso la url al httpclient
            client= new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
        }

        public async Task<bool> IniciarSesion(LoginDTO login)
        {
            if (string.IsNullOrWhiteSpace(login.Username)||string.IsNullOrWhiteSpace(login.Password))
            {
                throw new ArgumentException("Escriba el nombre de usuario o contraseña");
            }

            var request = await client.PostAsJsonAsync("api/login", login);
            if (request.IsSuccessStatusCode)
            {

                //read token
                var token = await request.Content.ReadFromJsonAsync<string>();


                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
