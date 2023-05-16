using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<ItesrcneDocentesContext>
    (optionsBuilder => optionsBuilder.UseMySql
    ("server=204.93.216.11;database=itesrcne_docentes;user=itesrcne_docente;password=docentes1",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb")));

//Datos para JWT
//Issuer, Audience, Secret
string issuer = "docentes.itesrc.net";
string audience = "mauidocentes";
var Secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DocentesKeyMoviles83G"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = Secret,
        ValidateAudience = true,
        ValidateIssuer = true,
    };
});


var app = builder.Build();


app.MapControllers();
app.UseAuthorization();
app.Run();
