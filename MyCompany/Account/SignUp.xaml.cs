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


    private void SigninAsync(object sender, EventArgs e)
    {
        username = usernamein.Text;
        password = passwordSignUp.Text;
        //passwordConfirmSignUp = passwordSignUp.Text;
    }
}

