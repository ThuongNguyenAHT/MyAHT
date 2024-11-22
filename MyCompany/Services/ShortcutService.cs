using MyCompany.Model;
using System.Net.Http.Json;

namespace MyCompany.Services;


public class ShortcutService
{
    HttpService httpService;
    public ShortcutService()
    {
        httpService = new HttpService();
    }
    List<Shortcut_Link> DocsList = new();
    public async Task<List<Shortcut_Link>> GetShortcutList()
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_string = $"@Shortcut@2019#";
        var input_encode = alphabet.EncodeBase64(input_string);
        var url = $"{serverUrl.url}/shortcutapi/getshortcutlist/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            DocsList = await response.Content.ReadFromJsonAsync<List<Shortcut_Link>>();
        }
        else
        {

        }
        return DocsList;

    }




}
