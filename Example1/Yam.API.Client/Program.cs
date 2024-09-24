using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Yam.API.Client.Config;
using Yam.Core.Extensions;
using Yam.API.Model.Dtos.Account.Request;
using Yam.API.Model.Dtos.Account;
using Yam.API.Model.Dtos.Account.Response;

public partial class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var serverOptions = GetServerOptions();

            var httpClient = new HttpClient();

            string token = string.Empty;

            do
            {
                token = TryLogin(httpClient, serverOptions.AccountUriLogin);
            }
            while (token == null);

            GetUserInfo(httpClient, token, serverOptions.AccountUriInfo);

        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: '{ex.Message}'\n");
        }

        Console.WriteLine("For exit press any key...");
        Console.ReadKey();
    }

    private static string TryLogin(HttpClient httpClient, string accountUriLogin)
    {
        var loginUserDTO = new LoginUserDTO();

        // to add registration

        Console.WriteLine("For login please input email:\n");
        loginUserDTO.Email = Console.ReadLine();

        Console.WriteLine("Please input password:\n");
        loginUserDTO.Password = Console.ReadLine();

        Console.WriteLine("Login process...\n");

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var authResponse = httpClient.PostAsync(accountUriLogin, loginUserDTO.ToHttpStringContent()).Result;

        var authResult = authResponse.Content.ReadAsAsync<AuthResult>().Result;

        if (!authResult.Success)
        {
            Console.WriteLine($"Error: '{string.Join(',', authResult.Errors)}'\n");
            return null;
        }

        return authResult.Token;
    }

    private static void GetUserInfo(HttpClient httpClient, string token, string accountUriInfo)
    {
        Console.WriteLine("Getting user info...\n");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        var infoResponse = httpClient.GetAsync(accountUriInfo).Result;

        var userInfo = infoResponse.Content.ReadAsAsync<UserResponseDTO>().Result;

        Console.WriteLine("Account info:");
        Console.WriteLine($"Email:    {userInfo.Email}");
        Console.WriteLine($"Username: {userInfo.Username}");
    }

    private static ServerOptions GetServerOptions()
    {
        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var config = builder.Build();

        var serverOptions = new ServerOptions();

        config.Bind("ServerOptions", serverOptions);

       var optionsWrapped = Options.Create(serverOptions);

        return optionsWrapped.Value;
    }
}