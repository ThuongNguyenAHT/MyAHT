using MyCompany.Account;
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

    // Phương thức xử lý sự kiện đăng ký
    private void OnSignUpTapped(object sender, EventArgs e) 
    { // Chuyển hướng đến trang đăng ký
      Navigation.PushAsync(new SignUp()); 
    }

    //Status Login
    async void SigninAsync(object sender, EventArgs e)
    {
        username = usernamein.Text;
        password = passwordin.Text;

        // Kiểm tra email và mật khẩu
        if (!IsValidEmail(username))
        {
            usernamein.TextColor = Colors.Red;
            emailErrorLabel.Text = "Email không hợp lệ. Vui lòng nhập lại";
            emailErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            usernamein.TextColor = Colors.Black;
            emailErrorLabel.IsVisible = false;
        }

        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            passwordin.TextColor = Colors.Red;
            passwordErrorLabel.Text = "Mật khẩu phải có ít nhất 8 ký tự";
            passwordErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            passwordin.TextColor = Colors.Black;
            passwordErrorLabel.IsVisible = false;
        }

        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            var result = await LoginService.OwinLogin(username, password);
            if (result.ToLower() == "true")
            {
                statusout.Text = "Login thành công!";
                statusout.TextColor = Colors.Blue;
                await Shell.Current.GoToAsync(nameof(Menu), true);
            }
            else
            {
                statusout.Text = "Email hoặc mật khẩu không đúng";
                statusout.TextColor = Colors.Red;
            }

            if (remember_but.IsChecked)
            {
                string name = "login.txt";
                string file = System.IO.Path.Combine(FileSystem.CacheDirectory, name);
                var content = alphabet.EncodeBase64(remember_but.IsChecked.ToString()) + Environment.NewLine + alphabet.EncodeBase64(username) + Environment.NewLine + alphabet.EncodeBase64(password);
                System.IO.File.WriteAllText(file, content);
            }
        }
        catch
        {
            statusout.Text = "Vui lòng kiểm tra lại kết nối mạng!";
            statusout.TextColor = Colors.Red;
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    { // Ẩn bàn phím
        if (sender is Entry entry) 
        { 
            entry.Unfocus(); 
        } 
    }

    //Envent completed for press enter key
    private void OnEntryCompleted(object sender, EventArgs e) 
    {
        if (sender is Entry entry)
        { 
            
            entry.Unfocus(); 
            
            SigninAsync(sender, e); 
        }
    }

    private void OnEmailTextChanged(object sender, TextChangedEventArgs e)
    {
        var email = e.NewTextValue;
        if (!IsValidEmail(email))
        {
            usernamein.TextColor = Colors.Red;
        }
        else
        {
            usernamein.TextColor = Colors.Black;
        }
    }

    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        var password = e.NewTextValue;
        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            passwordin.TextColor = Colors.Red;
        }
        else
        {
            passwordin.TextColor = Colors.Black;
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
}

