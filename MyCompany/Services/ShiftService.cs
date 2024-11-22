using MyCompany.Model;
using System.Net.Http.Json;

namespace MyCompany.Services;


public class ShiftService
{
    HttpService httpService;
    public ShiftService()
    {
        httpService = new HttpService();
    }
    List<Shift_info> DocsList = new();
    List<Shift_date_info> ShiftDateList = new();
    public async Task<List<Shift_info>> GetShiftList_today(string gio_in, string ngay_in, string thang_in, string nam_in)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_string = $"@Shift@2019#,{gio_in},{ngay_in},{thang_in},{nam_in}";
        var input_encode = alphabet.EncodeBase64(input_string);
        var url = $"{serverUrl.url}/shiftapi/getshiftlist_today/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            DocsList = await response.Content.ReadFromJsonAsync<List<Shift_info>>();
        }
        else
        {

        }
        return DocsList;

    }

    public async Task<List<Shift_info>> GetShiftList_yesterday(string gio_in, string ngay_in, string thang_in, string nam_in)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_string = $"@Shift@2019#,{gio_in},{ngay_in},{thang_in},{nam_in}";
        var input_encode = alphabet.EncodeBase64(input_string);
        var url = $"{serverUrl.url}/shiftapi/getshiftlist_yesterday/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            DocsList = await response.Content.ReadFromJsonAsync<List<Shift_info>>();
        }
        else
        {

        }
        return DocsList;

    }

    public async Task<List<Shift_date_info>> GetShiftList_date2date(string ngay_in_start, string thang_in_start, string nam_in_start, string ngay_in_end, string thang_in_end, string nam_in_end, string keysearch)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_string = $"@Shift@2019#,{ngay_in_start},{thang_in_start},{nam_in_start},{ngay_in_end},{thang_in_end},{nam_in_end},{keysearch}";
        var input_encode = alphabet.EncodeBase64(input_string);
        var url = $"{serverUrl.url}/shiftapi/getshiftlist_date2date/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            ShiftDateList = await response.Content.ReadFromJsonAsync<List<Shift_date_info>>();
        }
        else
        {

        }
        return ShiftDateList;

    }
}
