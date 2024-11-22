﻿using MyCompany.Model;
using System.Web;

namespace MyCompany.Services;


public class LoginService
{
    HttpService httpService;
    public LoginService()
    {
        httpService = new HttpService();
    }

    public async Task<string> OwinLogin(string username_in, string password_in)
    {
        ServerUrl serverUrl = new ServerUrl();
        var username_in_encode = alphabet.EncodeBase64($"{username_in},{password_in}");
        var password_in_encode = HttpUtility.UrlEncode(password_in);
        var url = $"{serverUrl.url}/loginapi/Login/{username_in_encode}";
        var response = await httpService.httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            return "false";
        }


    }

}
