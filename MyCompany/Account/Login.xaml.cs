namespace MyCompany;

public partial class Login : ContentPage
{
    public string username;
    public string password;
    public Login()
    {

        InitializeComponent();
    }


    private void SigninAsync(object sender, EventArgs e)
    {
        username = usernamein.Text;
        password = passwordin.Text;


    }
}

