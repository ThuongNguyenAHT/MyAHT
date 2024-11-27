namespace MyCompany.Account;

public partial class SignUp : ContentPage
{
    public string username;
    public string password;
    public string passwordConfirmSignUp;
    public SignUp()
    {

        InitializeComponent();
    }

    // Phương thức xử lý sự kiện với đúng chữ ký
    private void OnSignInTapped(object sender, EventArgs e) 
    { 
        // Chuyển hướng đến trang đăng nhập
        Navigation.PushAsync(new MainPage()); 
    }

    private void SigninAsync(object sender, EventArgs e)
    {
        username = usernameSignUp.Text;
        password = passwordSignUp.Text;
        //passwordConfirmSignUp = passwordSignUp.Text;
    }
}

