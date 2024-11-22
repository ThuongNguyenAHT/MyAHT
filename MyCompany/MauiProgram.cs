using MyCompany.Services;
using MyCompany.Views;

namespace MyCompany;
//date: 22/11
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
                fonts.AddFont("Sitka.ttc", "Sitka");
                fonts.AddFont("cambria.ttc", "Cambria");
                fonts.AddFont("cambriab.ttf", "Cambriabold");
                fonts.AddFont("cambriai.ttf", "Cambriaitalic");
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                fonts.AddFont("Roboto-Italic.ttf", "RobotoItalic");
                fonts.AddFont("Roboto-Black.ttf.ttf", "RobotoBlackBold");
            });
        builder.Services.AddTransient<Login>();
        builder.Services.AddTransient<Menu>();
        builder.Services.AddTransient<Flights>();
        builder.Services.AddTransient<DMS_Search>();
        builder.Services.AddTransient<DMS_Detail>();
        builder.Services.AddTransient<Shortcut>();
        builder.Services.AddTransient<Shift_Search>();
        builder.Services.AddSingleton<LoginService>();
        builder.Services.AddSingleton<DmsService>();
        builder.Services.AddSingleton<Flight_infoService>();
        builder.Services.AddSingleton<ShortcutService>();
        builder.Services.AddSingleton<ShiftService>();
        return builder.Build();
    }
}
