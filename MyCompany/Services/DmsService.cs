using MyCompany.Model;
using System.Net.Http.Json;

namespace MyCompany.Services;


public class DmsService
{
    HttpService httpService;
    public DmsService()
    {
        httpService = new HttpService();
    }
    List<DMS> DocsList = new();

    public async Task<List<DMS>> GetDocs(string Dms_id_in, string Dms_name_in, string Dms_uploaddate_in, string Dms_searchkey_in, string Dms_department_in, string Dms_catalog_in, string Dms_company_in, string Dms_author_in)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_string = $"@Dms@2019#,{Dms_id_in},{Dms_name_in},{Dms_uploaddate_in},{Dms_searchkey_in},{Dms_department_in},{Dms_catalog_in},{Dms_company_in},{Dms_author_in}";
        var input_encode = alphabet.EncodeBase64(input_string);
        var url = $"{serverUrl.url}/dmsapi/getdocs/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            DocsList = await response.Content.ReadFromJsonAsync<List<DMS>>();
        }
        else
        {

        }
        return DocsList;

    }

    public async Task<bool> GetDocsAuthority(string username, string Dms_id_in)
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_encode = alphabet.EncodeBase64($"@Dms@2019#,{username},{Dms_id_in}");
        var url = $"{serverUrl.url}/dmsapi/getauthority/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        bool output = false;
        if (response.IsSuccessStatusCode)
        {
            var text = await response.Content.ReadAsStringAsync();
            if (text.ToLower() == "true")
            {
                output = true;
            }
            else
            {
                output = false;
            }
        }
        else
        {
            return false;
        }
        return output;

    }
    public async Task<bool> GetDoc(DMS Dms)
    {
        ServerUrl serverUrl = new ServerUrl();
        string[] path_aray = Dms.path.Split("\\");
        string FileName = path_aray[path_aray.Length - 1];
        var input_encode = alphabet.EncodeBase64($"@Dms@2019#,{Dms.path}");
        var url = $"{serverUrl.url}/dmsapi/getdoc/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            Stream text = await response.Content.ReadAsStreamAsync();
            string file = System.IO.Path.Combine(FileSystem.CacheDirectory, FileName);

            using (var fs = new FileStream(file, FileMode.Create))
            {
                await text.CopyToAsync(fs);
            }
            await Launcher.Default.OpenAsync(new OpenFileRequest(file, new ReadOnlyFile(file)));
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<string>> GetDepts()
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_encode = alphabet.EncodeBase64($"@Dms@2019#");
        var url = $"{serverUrl.url}/dmsapi/getdepts/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        List<string> DeptsList = new();
        if (response.IsSuccessStatusCode)
        {
            DeptsList = await response.Content.ReadFromJsonAsync<List<string>>();
        }
        else
        {

        }
        return DeptsList;

    }

    public async Task<List<string>> GetCatalogs()
    {
        ServerUrl serverUrl = new ServerUrl();
        var input_encode = alphabet.EncodeBase64($"@Dms@2019#");
        var url = $"{serverUrl.url}/dmsapi/getcatalog/{input_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        List<string> CatalogsList = new();
        if (response.IsSuccessStatusCode)
        {
            CatalogsList = await response.Content.ReadFromJsonAsync<List<string>>();
        }
        else
        {

        }
        return CatalogsList;

    }


}
