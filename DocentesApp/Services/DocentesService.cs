using DocentesApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.Services
{
    public class DocentesService
    {
        public string url = "https://docentes.itesrc.net/";
        private readonly AuthService auth;
        private readonly LoginService login;
        HttpClient client;

        public DocentesService(AuthService auth, LoginService login)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            this.auth = auth;
            this.login = login;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + auth.ReadToken().Result);
        }

        public async Task<List<Docente>> Get()
        {
            var response = await client.GetAsync("api/docentes");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Docente>>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                login.Logout();
            }

            return null;
        }
    }
}
