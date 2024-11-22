using Microsoft.Maui.Controls;
using MyCompany.Model;
using MyCompany.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyCompany;

public partial class Shift_Search : ContentPage
{
    ShiftService shiftService = new ShiftService();
    public ObservableCollection<Shift_info> Shift_infos { get; } = new();
    public ObservableCollection<Shift_date_info> tab2_Shift_infos { get; } = new();
    public Shift_Search()
    {
        InitializeComponent();
        DateTime today = new DateTime();
        today = DateTime.Now;
        time_picker.Time = today.TimeOfDay;
    }

    async void GetShiftList(object sender, EventArgs e)
    {
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            var hour_in = time_picker.Time.Hours.ToString("d2") + ":" + time_picker.Time.Minutes.ToString("d2");
            var date_in = date_picker.Date.Day.ToString("d2");
            var month_in = date_picker.Date.Month.ToString("d2");
            var year_in = date_picker.Date.Year.ToString("d2");
            Shift_infos.Clear();
            var shifts_today = await shiftService.GetShiftList_today(hour_in, date_in, month_in, year_in);
            var index = 1;
            foreach (var shift in shifts_today)
            {
                ServerUrl serverUrl = new ServerUrl();

                shift.index = index;
                index++;
                Shift_infos.Add(shift);
            }
            var shifts_yesterday = await shiftService.GetShiftList_yesterday(hour_in, date_in, month_in, year_in);
            foreach (var shift in shifts_yesterday)
            {
                ServerUrl serverUrl = new ServerUrl();

                shift.index = index;
                shift.ShiftCodeVN = shift.ShiftCodeVN + "-" + "Ca hôm trước";
                index++;
                Shift_infos.Add(shift);
            }
            shiftcollectionview.ItemsSource = Shift_infos;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Shifts: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }
    }

    //Tab 2 function
    async void tab2_GetShiftList(object sender, EventArgs e)
    {
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            tab2_ActivityIndicatorin.IsVisible = IsBusy;
            tab2_ActivityIndicatorin.IsRunning = IsBusy;

            var date_in_start = tab2_date_picker_start.Date.Day.ToString("d2");
            var month_in_start = tab2_date_picker_start.Date.Month.ToString("d2");
            var year_in_start = tab2_date_picker_start.Date.Year.ToString("d2");
            var date_in_end = tab2_date_picker_end.Date.Day.ToString("d2");
            var month_in_end = tab2_date_picker_end.Date.Month.ToString("d2");
            var year_in_end = tab2_date_picker_end.Date.Year.ToString("d2");
            tab2_Shift_infos.Clear();
            var shifts_today = await shiftService.GetShiftList_date2date(date_in_start, month_in_start, year_in_start, date_in_end, month_in_end, year_in_end, tab2_name_in.Text);
            var index = 1;
            foreach (var shift in shifts_today)
            {
                ServerUrl serverUrl = new ServerUrl();

                shift.index = index;
                index++;
                tab2_Shift_infos.Add(shift);
            }
            tab2_shiftcollectionview.ItemsSource = tab2_Shift_infos;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Shifts: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            tab2_ActivityIndicatorin.IsVisible = IsBusy;
            tab2_ActivityIndicatorin.IsRunning = IsBusy;
        }
    }

    
    //Common Function
    async void tab1_visible(object sender, EventArgs e)
    {
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
            tab1.IsVisible = true;
            tab2.IsVisible = false;
            tab1_but.Style = (Style)Application.Current.Resources["ButtonOutline_selected"];
            tab2_but.Style = (Style)Application.Current.Resources["ButtonOutline_unselected"];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Shifts: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            ActivityIndicatorin.IsVisible = IsBusy;
            ActivityIndicatorin.IsRunning = IsBusy;
        }
    }
    async void tab2_visible(object sender, EventArgs e)
    {
        if (IsBusy == true)
            return;
        try
        {
            IsBusy = true;
            tab2_ActivityIndicatorin.IsVisible = IsBusy;
            tab2_ActivityIndicatorin.IsRunning = IsBusy;
            tab2.IsVisible = true;
            tab1.IsVisible = false;
            tab2_but.Style = (Style)Application.Current.Resources["ButtonOutline_selected"];
            tab1_but.Style = (Style)Application.Current.Resources["ButtonOutline_unselected"];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Shifts: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            tab2_ActivityIndicatorin.IsVisible = IsBusy;
            tab2_ActivityIndicatorin.IsRunning = IsBusy;
        }
    }

}
