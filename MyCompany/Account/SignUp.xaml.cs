namespace MyCompany.Account;

public partial class SignUp : ContentPage
{
    public string firstName;
    public string lastName;
    public string username;
    public string password;
    public string confirmPassword;
    public string phoneNumber;
    public DateTime dateOfBirth;
    public SignUp()
    {
        InitializeComponent();
    }

    private void OnSignInTapped(object sender, EventArgs e) 
    { 
        // Chuyển hướng đến trang đăng nhập
        Navigation.PushAsync(new MainPage()); 
    }

    private void SigninAsync(object sender, EventArgs e)
    {
        username = usernameSignUp.Text;
        password = passwordSignUp.Text;
        firstName = firstNameSignUp.Text;
        lastName = lastNameSignUp.Text;
        confirmPassword = passwordConfirmSignUp.Text;
        phoneNumber = phoneSignUp.Text;
        dateOfBirth = dateOfBirthSignUp.Date;
    }

    private void OnPhoneTextChanged(object sender, TextChangedEventArgs e)
    {
        // Xác thực số điện thoại chỉ cho phép nhập số điện thoại với mã quốc gia (+84)
        string newText = e.NewTextValue;

        
        if (newText.Any(c => !char.IsDigit(c) && c != '+') && newText.Length > 10)
        {
            ((Entry)sender).TextColor = Colors.Red; // Đổi màu chữ thành màu đỏ nếu không hợp lệ
        }
        else
        {
            ((Entry)sender).TextColor = Colors.Black; // Đổi lại màu chữ mặc định nếu hợp lệ
        }
    }
}

