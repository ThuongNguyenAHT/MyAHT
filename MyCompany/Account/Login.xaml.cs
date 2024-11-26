using MyCompany.Account;

namespace MyCompany;

public partial class Login : ContentPage
{
    public string username;
    public string password;
    public Login()
    {

        InitializeComponent();
    }
    private void OnSignUpTapped(object sender, EventArgs e)
    { // Chuyển hướng đến trang đăng ký
        Navigation.PushAsync(new SignUp());
    }

    private void SigninAsync(object sender, EventArgs e)
    {
        username = usernamein.Text;
        password = passwordin.Text;

    }

    

}

