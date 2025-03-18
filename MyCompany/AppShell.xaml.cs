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
    public void Logout()
    {
        try
        {
            // Xóa thông tin đăng nhập đã lưu
            string name = "login.txt";
            string file = System.IO.Path.Combine(FileSystem.CacheDirectory, name);
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }

            // Đặt lại MainPage về trang đăng nhập
            Application.Current.MainPage = new MainPage();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi đăng xuất: " + ex.Message);
        }
    }
}
