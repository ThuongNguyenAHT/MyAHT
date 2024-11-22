using MyCompany.Model;
using System.Net.Http.Json;

namespace MyCompany.Services;


public class Flight_infoService
{
    HttpService httpService;
    public Flight_infoService()
    {
        httpService = new HttpService();
    }
    List<Flight_info> Flight_infoList = new();
    public async Task<List<Flight_info>> getflights(string fl_date, string fl_month, string fl_year, string fl_adi)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_encode = alphabet.EncodeBase64($"{fl_date},{fl_month},{fl_year},{fl_adi}");
        var url = $"{serverUrl.url}/flightapi/getflights/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            Flight_infoList = await response.Content.ReadFromJsonAsync<List<Flight_info>>();
        }
        else
        {

        }
        return Flight_infoList;

    }

    public async Task<List<Flight_info>> getflightswithkey(string fl_date, string fl_month, string fl_year, string fl_adi, string fl_key)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_encode = alphabet.EncodeBase64($"{fl_date},{fl_month},{fl_year},{fl_adi},{fl_key}");
        var url = $"{serverUrl.url}/flightapi/getflightswithkey/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            Flight_infoList = await response.Content.ReadFromJsonAsync<List<Flight_info>>();
        }
        else
        {

        }
        return Flight_infoList;

    }

}
