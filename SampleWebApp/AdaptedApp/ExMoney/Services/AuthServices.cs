

namespace ExMoney.Services;

public class AuthService
{
    private string baseAddress = null;
    public AuthService(IConfiguration configuration)
    {
        baseAddress = configuration["AuthBaseAddress"];
    }

    public async Task<string> Login(string username, string password, bool isEmail)
    {
        HttpClient client = new HttpClient(){
            BaseAddress = new Uri(baseAddress)
        };

        var response = await client.PostAsJsonAsync("/api/v1/users/login", new { username, password, isEmail } );
        
        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else 
            return null;        
    }

    public async Task Register(){


    }
}
