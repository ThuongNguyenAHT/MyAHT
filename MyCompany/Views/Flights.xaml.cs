using MyCompany.Model;
using MyCompany.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyCompany;

public partial class Flights : ContentPage
{
    Flight_infoService flight_InfoService = new Flight_infoService();
    DateTime flight_date = new DateTime();
    public ObservableCollection<Flight_info> Flight_infos { get; } = new();
    public Flights()
    {
        InitializeComponent();
        flight_date = DateTime.Now;
        date_out.Text = flight_date.ToString("dd/MM/yyyy");
        //Shell.SetForegroundColor(this,Colors.Transparent);
    }

    async void GetDepartureFlights(object sender, EventArgs e)
    {
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            toolbarimage.Source = ImageSource.FromFile("dep_icon.png");
            Departure_but.Style = (Style)Application.Current.Resources["ButtonOutline_selected"];
            Arrival_but.Style = (Style)Application.Current.Resources["ButtonOutline_unselected"];

            Flight_infos.Clear();
            var flights = await flight_InfoService.getflightswithkey(flight_date.Day.ToString("d2"), flight_date.Month.ToString("d2"), flight_date.Year.ToString("d2"), "D", text_in.Text);
            var index = 1;

            foreach (var flight in flights)
            {
                ServerUrl serverUrl = new ServerUrl();
                flight.logo = $"{serverUrl.url}/SITA_Interface/Airline_logo/{flight.linecode}.jpg";
                //flight.flight_logo_source = await LoadImageUrl(flight.logo);
                flight.logo = await ImageSaveAndLink(flight.logo, flight.linecode + ".jpg");
                flight.display_icon1 = "gateicon.png";
                flight.display_icon1_value = flight.gate;
                flight.display_icon2 = "checkin_icon.png";
                var checkin_array = flight.CheckInCounters.Split(",");
                flight.display_icon2_value = checkin_array[0] + "->" + checkin_array[checkin_array.Length - 1];
                flight.schedule_display = flight.schedule.ToString("dd/MM/yyyy H:mm");
                flight.mostconfident_display = flight.mostconfident.ToString("dd/MM/yyyy H:mm");
                flight.index = index;
                index++;
                Flight_infos.Add(flight);
            }
            flightcollectionview.ItemsSource = Flight_infos;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!GetDepartureFlights", "Vui lòng chụp màn hình và gửi qua zalo: 0905811839!", "Ok");
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
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

    async void GetArrivalFlights(object sender, EventArgs e)
    {
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            toolbarimage.Source = "arr_icon.png";
            Flight_infos.Clear();
            Departure_but.Style = (Style)Application.Current.Resources["ButtonOutline_unselected"];
            Arrival_but.Style = (Style)Application.Current.Resources["ButtonOutline_selected"];
            var flights = await flight_InfoService.getflightswithkey(flight_date.Day.ToString("d2"), flight_date.Month.ToString("d2"), flight_date.Year.ToString("d2"), "A", text_in.Text);


            var index = 1;
            foreach (var flight in flights)
            {
                ServerUrl serverUrl = new ServerUrl();
                flight.logo = $"{serverUrl.url}/SITA_Interface/Airline_logo/{flight.linecode}.jpg";
                //flight.flight_logo_source = await LoadImageUrl(flight.logo);
                flight.logo = await ImageSaveAndLink(flight.logo, flight.linecode + ".jpg");

                flight.display_icon1 = "baggageclaim_icon.png";
                flight.display_icon1_value = flight.claim;
                flight.display_icon2 = "stand_icon.png";
                flight.display_icon2_value = flight.DailyUpdateStand.Replace("Stand", "");
                flight.schedule_display = flight.schedule.ToString("dd/MM/yyyy H:mm");
                flight.mostconfident_display = flight.mostconfident.ToString("dd/MM/yyyy H:mm");
                flight.index = index;
                index++;
                Flight_infos.Add(flight);
            }

            flightcollectionview.ItemsSource = Flight_infos;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!GetArrivalFlights", "Vui lòng chụp màn hình và gửi qua zalo: 0905811839!", "Ok");
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }
    }

    void NextDate(object sender, EventArgs e)
    {
        flight_date = flight_date.AddDays(1);
        flightcollectionview.ItemsSource = null;
        date_out.Text = flight_date.ToString("dd/MM/yyyy");
        return;
    }
    void BackDate(object sender, EventArgs e)
    {
        flight_date = flight_date.AddDays(-1);
        flightcollectionview.ItemsSource = null;
        date_out.Text = flight_date.ToString("dd/MM/yyyy");
        return;
    }

}
