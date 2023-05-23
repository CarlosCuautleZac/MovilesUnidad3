using DocentesApp.Services;
using Microsoft.Extensions.Logging;

namespace DocentesApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Formas de inyectar un object

            //Singletoon => se crea una solo instancia para una sola aplicacion
            //Transient => Cada viewmodel recibe cada version de este
            //Scopeed => defines un limite por cada instancia

            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<AuthService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}