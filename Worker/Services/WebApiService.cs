namespace Worker.Services;

public class WebApiService
{
    // private const string GovernmentDirectoryServiceUrl = "http://company-api:8080";
    // private const string BackgroundCheckServiceUrl = "http://background-check-api:8080";
    // private const string CompanyServiceUrl = "http://government-directory-api:8080";

    private const string GovernmentDirectoryServiceUrl = "http://localhost:5004";
    private const string BackgroundCheckServiceUrl = "http://localhost:5003";
    private const string CompanyServiceUrl = "http://localhost:5002";

    private readonly HttpClient _httpClient;

    public WebApiService()
    {
        _httpClient = new HttpClient();
    }
    public async Task<Guid> GetGovernmentEmployeeId(Guid employeeId)
    {
        string endpoint = $"{CompanyServiceUrl}/api/Employee/{employeeId}";
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

    public async Task<Guid> GetSSN(Guid governmentDirectoryId)
    {
        string endpoint = $"{GovernmentDirectoryServiceUrl}/api/EmployeeInformation?GovernmentDirectoryId={governmentDirectoryId}";
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

    public async Task<string> StartBackgroundCheck(Guid SSN)
    {
        string endpoint = $"{BackgroundCheckServiceUrl}/api/BackgroundCheck?SocialSecurityNumber={SSN}";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        else
        {
            Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            return string.Empty;
        }
    }

    public async Task<bool> GetTrafficViolation(Guid SSN)
    {
        string endpoint = $"{BackgroundCheckServiceUrl}/api/BackgroundCheck/GetTrafficViolation?SocialSecurityNumber={SSN}";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();

            if (bool.TryParse(responseString, out bool result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Unexpected response content");
                throw new Exception("Response is not bool");
            }
        }
        else
        {
            Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            throw new Exception($"Request failed with status code: {response.StatusCode}");
        }
    }

    public async Task<bool> GetCivilOffence(Guid SSN)
    {
        string endpoint = $"{BackgroundCheckServiceUrl}/api/BackgroundCheck/GetCivilOffence?SocialSecurityNumber={SSN}";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();

            if (bool.TryParse(responseString, out bool result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Unexpected response content");
                throw new Exception("Response is not bool");
            }
        }
        else
        {
            Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            throw new Exception($"Request failed with status code: {response.StatusCode}");
        }
    }

    public async Task<bool> GetCriminalRecord(Guid SSN)
    {
        string endpoint = $"{BackgroundCheckServiceUrl}/api/BackgroundCheck/GetCriminalRecord?SocialSecurityNumber={SSN}";
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();

            if (bool.TryParse(responseString, out bool result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Unexpected response content");
                throw new Exception("Response is not bool");
            }
        }
        else
        {
            Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            throw new Exception($"Request failed with status code: {response.StatusCode}");
        }
    }


}
