using Microsoft.Maui.Controls;
using MyCompany.ViewsSettingFunction;

namespace MyCompany.Views
{
    public partial class Setting : ContentPage
    {
        public Setting()
        {
            InitializeComponent();
        }

        private async void OnAccountManagementClicked(object sender, EventArgs e)
        {
            // Chuyển đến trang Quản lý tài khoản
            await Navigation.PushAsync(new AccountManagementPage());
        }

        private async void OnThemeLanguageSelectionClicked(object sender, EventArgs e)
        {
            // Chuyển đến trang Chọn giao diện & ngôn ngữ
            await Navigation.PushAsync(new ThemeLanguageSelectionPage());
        }

        private async void OnNotificationSettingsClicked(object sender, EventArgs e)
        {
            // Chuyển đến trang Cài đặt thông báo
            await Navigation.PushAsync(new NotificationSettingsPage());
        }


        private async void OnBackupRestoreClicked(object sender, EventArgs e)
        {
            // Chuyển đến trang Sao lưu và phục hồi
            await Navigation.PushAsync(new BackupRestorePage());
        }

        private async void OnUserGuideClicked(object sender, EventArgs e)
        {
            // Chuyển đến trang Hướng dẫn sử dụng
            await Navigation.PushAsync(new UserGuidePage());
        }

        private async void OnTransferAccountClicked(object sender, EventArgs e)
        {
            // Chuyển đến trang Câu hỏi thường gặp
            await Navigation.PushAsync(new TransferAccountPage());
        }
    }
}
