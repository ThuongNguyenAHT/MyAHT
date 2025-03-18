using MyCompany.Views;

namespace MyCompany;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}
    private void OnLogoutClicked(object sender, EventArgs e)
    {
        (Application.Current.MainPage as AppShell)?.Logout();
    }




}