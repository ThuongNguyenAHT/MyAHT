using MyCompany.Views;

namespace MyCompany;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Login), typeof(Login));
        Routing.RegisterRoute(nameof(Menu), typeof(Menu));
        Routing.RegisterRoute(nameof(Flights), typeof(Flights));
        Routing.RegisterRoute(nameof(DMS_Search), typeof(DMS_Search));
        Routing.RegisterRoute(nameof(DMS_Detail), typeof(DMS_Detail));
        Routing.RegisterRoute(nameof(Shortcut), typeof(Shortcut));
        Routing.RegisterRoute(nameof(Shift_Search), typeof(Shift_Search));
    }
}
