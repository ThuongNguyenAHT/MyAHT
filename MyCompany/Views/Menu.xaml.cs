using Microsoft.Maui.Primitives;
using MyCompany.Model;
using MyCompany.Services;
using MyCompany.Views;
using System.Collections.ObjectModel;

namespace MyCompany;

public partial class Menu : ContentPage
{
    public string username;
    public string password;
    public ObservableCollection<E_News> E_News_list { get; } = new();
    public ObservableCollection<E_News> E_Survey_list { get; } = new();
    public ObservableCollection<E_News> E_Other_list { get; } = new();
    public Menu()
    {

        InitializeComponent();
        update_e_newsAsync();
    }

    async void update_e_newsAsync()
    {
        ENewsService eNewsService = new ENewsService();
        //If return is 0, label is off, collection view is off and label is off

        try
        {
            E_News_list.Clear();
            var News_List = await eNewsService.GetNews_News(MainPage.username, "001");
            if (News_List.Count == 0)
            {
                e_news_newscollection_label.IsVisible = false;
                e_news_newscollection.IsVisible = false;
            }
            else
            {
                foreach (var news in News_List)
                {
                    news.E_News_imagepath = await ImageSaveAndLink(news.E_News_imagepath, news.E_News_title);
                    E_News_list.Add(news);
                }
                e_news_newscollection_label.IsVisible = true;
                e_news_newscollection.IsVisible = true;
                e_news_newscollection.ItemsSource = E_News_list;
            }
        }
        catch
        {
            e_news_newscollection_label.IsVisible = false;
            e_news_newscollection.IsVisible = false;
        }


        try
        {
            E_Survey_list.Clear();
            var Survey_List = await eNewsService.GetNews_News(MainPage.username, "002");
            if (Survey_List.Count == 0)
            {
                e_news_survey_label.IsVisible = false;
                e_news_survey.IsVisible = false;
            }
            else
            {
                foreach (var news in Survey_List)
                {
                    news.E_News_imagepath = await ImageSaveAndLink(news.E_News_imagepath, news.E_News_title);
                    E_Survey_list.Add(news);
                }
                e_news_survey_label.IsVisible = true;
                e_news_survey.IsVisible = true;
                e_news_survey.ItemsSource = E_Survey_list;
            }
        }
        catch
        {
            e_news_survey_label.IsVisible = false;
            e_news_survey.IsVisible = false;
        }
        
        try
        {
            E_Other_list.Clear();
            var Other_List = await eNewsService.GetNews_News(MainPage.username, "003");
            if (Other_List.Count == 0)
            {
                e_news_other_label.IsVisible = false;
                e_news_other.IsVisible = false;
            }
            else
            {
                foreach (var news in Other_List)
                {
                    news.E_News_imagepath = await ImageSaveAndLink(news.E_News_imagepath, news.E_News_title);
                    E_Other_list.Add(news);
                }
                e_news_other_label.IsVisible = true;
                e_news_other.IsVisible = true;
                e_news_other.ItemsSource = E_Other_list;
            }
        }
        catch
        {
            e_news_other_label.IsVisible = false;
            e_news_other.IsVisible = false;
        }

    }
    void routeAsync(object sender, EventArgs e)
    {
        Frame frame = (Frame)sender;
        var button_link_temp = (button_link)frame.BindingContext;
        if (button_link_temp.page_route == "Shortcut")
            Shell.Current.GoToAsync(nameof(Shortcut), true);
        else if (button_link_temp.page_route == "Flights")
            Shell.Current.GoToAsync(nameof(Flights), true);
        else if (button_link_temp.page_route == "DMS_Search")
            Shell.Current.GoToAsync(nameof(DMS_Search), true);
        else if (button_link_temp.page_route == "Shift_Search")
            Shell.Current.GoToAsync(nameof(Shift_Search), true);

    }
    async void E_NEWS_Async(object sender, EventArgs e)
    {
        Frame frame = (Frame)sender;
        var Shortcut_temp = (E_News)frame.BindingContext;
        if (Shortcut_temp.E_News_link != "")
        {
            //bool launcherOpened = await Launcher.Default.TryOpenAsync(Shortcut_temp.Shortcut_name_url, BrowserLaunchMode.SystemPreferred);
            try
            {
                Uri uri = new Uri(Shortcut_temp.E_News_link);
                BrowserLaunchOptions options = new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Colors.DarkBlue,
                    PreferredControlColor = Colors.White
                };

                await Browser.Default.OpenAsync(uri, options);
            }
            //catch (Exception ex)
            catch
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }
    }

    async Task<string> ImageSaveAndLink(string url, string file_name)
    {
        string file = System.IO.Path.Combine(FileSystem.CacheDirectory, file_name);
        if (System.IO.File.Exists(file) != true)
        {
            HttpService myHttpService = new HttpService();
            var imagestream = myHttpService.httpClient.GetStreamAsync(url).Result;

            try
            {
                using (var fs = new FileStream(file, FileMode.CreateNew))
                {
                    await imagestream.CopyToAsync(fs);
                }
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error!", "Vui lòng chụp màn hình và gửi qua zalo: 0905811839!", "Ok");

            }
        }

        return file;


    }

}

