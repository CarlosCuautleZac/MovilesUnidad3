using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocentesApp.Services
{
    public class AuthService
    {
        //Normalmente tiene 3 metodos clasicos
        string token;


        //Write
        public async void WriteToken(string token)
        {
            this.token = token;
            await SecureStorage.SetAsync("JwtToken", token);
        }

        //Read

        public async Task<string> ReadToken()
        {
            token = await SecureStorage.GetAsync("JwtToken");
            return token;
        }

        //Remove
        public void RemoveToken()
        {
            SecureStorage.Remove("JwtToken");
            token = null;
            token = null;
        }
    }
}
