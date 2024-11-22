using MyCompany.Model;
using MyCompany.Services;
using System.Collections.ObjectModel;


namespace MyCompany;

public partial class Shortcut : ContentPage
{
    ShortcutService shortcutService = new ShortcutService();
    public ObservableCollection<Shortcut_Link> Shortcutlist { get; } = new();
    public Shortcut()
    {
        InitializeComponent();
        try
        {
            var ShortcutList_temp = Task.Run(() => shortcutService.GetShortcutList()).Result;

            Alphabet my_alplabet_list = new Alphabet();

            foreach (string letter in my_alplabet_list.Alphabet_array)
            {
                Shortcut_Link start_letter = new Shortcut_Link();
                start_letter.Shortcut_name = letter.ToUpper();
                start_letter.Shortcut_name_url = "";
                start_letter.Shortcut_name_isheader = true;
                var first_match = true;
                foreach (var temp in ShortcutList_temp)
                {
                    if (temp.Shortcut_name.ToLower().StartsWith(letter.ToLower()) == true)
                    {
                        if (first_match == true)
                        {
                            Shortcutlist.Add(start_letter);
                            first_match = false;
                        }
                        Shortcutlist.Add(temp);
                    }

                }
            }
            shortcutcollectionview.ItemsSource = Shortcutlist;
        }
        catch
        {
            Shell.Current.DisplayAlert("Error!", "Vui lòng sử dụng mạng nội bộ để truy cập!", "Ok");
        }

    }

    async void GoToLinkAsync(object sender, EventArgs e)
    {
        Frame frame = (Frame)sender;
        var Shortcut_temp = (Shortcut_Link)frame.BindingContext;
        if (Shortcut_temp.Shortcut_name_url != "")
        {
            //bool launcherOpened = await Launcher.Default.TryOpenAsync(Shortcut_temp.Shortcut_name_url, BrowserLaunchMode.SystemPreferred);
            try
            {
                Uri uri = new Uri(Shortcut_temp.Shortcut_name_url);
                BrowserLaunchOptions options = new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Colors.Blue,
                    PreferredControlColor = Colors.White
                };

                await Browser.Default.OpenAsync(uri, options);
            }
            //catch (Exception ex)
            //catch (Exception ex)
            catch 
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }



    }
}