using MyCompany.Services;

namespace MyCompany;



public partial class MainPage : ContentPage
{

    public static string username { get; set; }
    public string password;
    public bool IsBusy;
    LoginService LoginService;
    public MainPage()
    {

        InitializeComponent();
        LoginService = new LoginService();
        IsBusy = false;
        //Shell.Current.GoToAsync(nameof(DMS_Search), true);
        try
        {
            string name = "login.txt";
            string file = System.IO.Path.Combine(FileSystem.CacheDirectory, name);
            string content = System.IO.File.ReadAllText(file);
            var content_array = content.Split(Environment.NewLine);
            if (alphabet.DecodeBase64(content_array[0]).ToLower() == "true")
            {
                remember_but.IsChecked = true;
            }
            else
            {
                remember_but.IsChecked = false;
            }

            usernamein.Text = alphabet.DecodeBase64(content_array[1]);
            passwordin.Text = alphabet.DecodeBase64(content_array[2]);
        }
        //catch (Exception ex)
        catch 
        {
            string name = "login.txt";
            string file = System.IO.Path.Combine(FileSystem.CacheDirectory, name);
            var content = alphabet.EncodeBase64(remember_but.IsChecked.ToString()) + Environment.NewLine + alphabet.EncodeBase64(username) + Environment.NewLine + alphabet.EncodeBase64(password);
            System.IO.File.WriteAllText(file, content);
        }


    }


    async void SigninAsync(object sender, EventArgs e)
    {
        username = usernamein.Text;
        password = passwordin.Text;
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            var result = await LoginService.OwinLogin(username, password);
            if (result.ToLower() == "true")
            {
                statusout.Text = "Login Success!";
                statusout.TextColor = Colors.Blue;
                await Shell.Current.GoToAsync(nameof(Menu), true);
            }
            else
            {
                statusout.Text = "Failed To Login!";
                statusout.TextColor = Colors.Red;
            }
            if (remember_but.IsChecked == true)
            {
                string name = "login.txt";
                string file = System.IO.Path.Combine(FileSystem.CacheDirectory, name);
                var content = alphabet.EncodeBase64(remember_but.IsChecked.ToString()) + Environment.NewLine + alphabet.EncodeBase64(username) + Environment.NewLine + alphabet.EncodeBase64(password);
                System.IO.File.WriteAllText(file, content);
            }

        }
        //catch (Exception ex)
        catch 
        {
            statusout.Text = $"Vui lòng kiểm tra lại kết nối mạng!";
            statusout.TextColor = Colors.Red;
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }


    }
}

