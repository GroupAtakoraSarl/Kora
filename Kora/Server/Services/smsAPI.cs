using Amazon.SecurityToken;

namespace Kora.Server.Services;


public interface IsmsAPI
{
    Task<string> SendSMSAsync(SMSRequest smsRequest);
}

public class smsAPI : IsmsAPI
{
    private readonly HttpClient _httpClient;

    public smsAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> SendSMSAsync(SMSRequest smsRequest)
    {
        var content =
            new StringContent(System.Text.Json.JsonSerializer.Serialize(smsRequest), null, "application/json");
        var response = await _httpClient.PostAsync("api/sendSMS", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}

public class SMSRequest
{
    public string From { get; set; }
    public long To { get; set; }
    public string Text { get; set; }
    public int Reference { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
}