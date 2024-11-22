using MyCompany.Model;
using System.Net.Http.Json;

namespace MyCompany.Services;


public class ENewsService
{
    HttpService httpService;
    public ENewsService()
    {
        httpService = new HttpService();
    }

    public async Task<List<E_News>> GetNews_News(string user_name, string E_News_Type_ID)
    {
        List<E_News> News_list = new List<E_News>();
        ServerUrl serverUrl = new ServerUrl();
        var input_string = $"@ENews@2019#,{user_name},{E_News_Type_ID}";
        var input_encode = alphabet.EncodeBase64(input_string);
        var url = $"{serverUrl.url}/enewsapi/GetNews/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            News_list = await response.Content.ReadFromJsonAsync<List<E_News>>();
        }
        else
        {

        }
        return News_list;

    }

    //public async Task<List<E_News>> GetNews_Surveys(string user_name)
    //{
    //
    //}
    //
    //public async Task<List<E_News>> GetNews_Other(string user_name)
    //{
    //
    //}


}
