namespace Worker.Services;

public class WebApiService
{
    private const string GovernmentDirectoryServiceUrl = "http://localhost:5004";
    private const string BackgroundCheckServiceUrl = "http://localhost:5003";
    private const string CompanyServiceUrl = "http://localhost:5002";

    private readonly HttpClient _httpClient;

    public WebApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Guid> GetSSN(Guid governmentDirectoryId)
    {
        _httpClient.BaseAddress = new Uri(GovernmentDirectoryServiceUrl);

        string endpoint = $"api/EmployeeInformation?GovernmentDirectoryId={governmentDirectoryId}";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string guidString = await response.Content.ReadAsStringAsync();

            // Trim any whitespace and remove any quotes
            guidString = guidString.Trim().Trim('"');

            if (Guid.TryParse(guidString, out Guid resultGuid))
            {
                Console.WriteLine($"Received GUID: {resultGuid}");
                return resultGuid;
            }
            else
            {
                Console.WriteLine("The response is not a valid GUID.");
                return Guid.Empty;
            }
        }
        else
        {
            Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            return Guid.Empty;
        }
    }
}
