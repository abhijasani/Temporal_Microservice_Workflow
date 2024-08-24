using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The first API endpoint URL
             Stopwatch stopwatch = new Stopwatch();
            DateTime startTime = DateTime.Now;
            stopwatch.Start(); // Start measuring time

            string firstResponseBody = "";
            string SecondResponseBody = "";
            string ThiredResponseBody1 = "";
            string ThiredResponseBody2 = "";
            string ThiredResponseBody3 = "";
            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Make the first API call
                    //string firstApiUrl = "http://localhost:5002/api/Employee/e71527b7-01e1-48b2-8f49-7b1ebe60afea";
                    string firstApiUrl = "http://localhost:5002/api/Employee/4a233c05-548e-4fec-8fbe-98732d235161";
                    

                    firstResponseBody = await GetApiResponseAsync(client, firstApiUrl);

                    if (firstResponseBody == string.Empty)
                    {
                        Console.WriteLine("Failed to get a valid response from the first API.");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                try
                {
                    // Trim the response and use it to make the second API call
                    string governmentDirectoryId = firstResponseBody.Trim('"');
                    if (!string.IsNullOrEmpty(governmentDirectoryId))
                    {
                        // Make the second API call
                        string secondApiUrl = $"http://localhost:5004/api/EmployeeInformation?GovernmentDirectoryId={governmentDirectoryId}";
                        SecondResponseBody = await GetApiResponseAsync(client, secondApiUrl);

                        if (SecondResponseBody == string.Empty)
                        {
                            Console.WriteLine("Failed to get a valid response from the Second API.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("GovernmentDirectoryId is empty after trimming.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                try
                {
                    // Trim the response and use it to make the second API call
                    string SocialSecurityNumber = SecondResponseBody.Trim('"');
                    if (!string.IsNullOrEmpty(SocialSecurityNumber))
                    {
                        // Make the second API call
                        string ThiredApiUrl1 = $"http://localhost:5003/api/BackgroundCheck/GetTrafficViolation?SocialSecurityNumber={SocialSecurityNumber}";
                        ThiredResponseBody1 = await GetApiResponseAsync(client, ThiredApiUrl1);

                        if (ThiredResponseBody1 == string.Empty)
                        {
                            Console.WriteLine("Failed to get a valid response from the Second API.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("GovernmentDirectoryId is empty after trimming.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }


                try
                {
                    // Trim the response and use it to make the second API call
                    string SocialSecurityNumber = SecondResponseBody.Trim('"');
                    if (!string.IsNullOrEmpty(SocialSecurityNumber))
                    {
                        // Make the second API call
                        string ThiredApiUrl2 = $"http://localhost:5003/api/BackgroundCheck/GetCivilOffence?SocialSecurityNumber={SocialSecurityNumber}";
                        ThiredResponseBody2 = await GetApiResponseAsync(client, ThiredApiUrl2);

                        if (ThiredResponseBody2 == string.Empty)
                        {
                            Console.WriteLine("Failed to get a valid response from the Second API.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("GovernmentDirectoryId is empty after trimming.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }


                try
                {
                    // Trim the response and use it to make the second API call
                    string SocialSecurityNumber = SecondResponseBody.Trim('"');
                    if (!string.IsNullOrEmpty(SocialSecurityNumber))
                    {
                        // Make the second API call
                        string ThiredApiUrl3 = $"http://localhost:5003/api/BackgroundCheck/GetCriminalRecord?SocialSecurityNumber={SocialSecurityNumber}";
                        ThiredResponseBody3 = await GetApiResponseAsync(client, ThiredApiUrl3);

                        if (ThiredResponseBody3 == string.Empty)
                        {
                            Console.WriteLine("Failed to get a valid response from the Second API.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("GovernmentDirectoryId is empty after trimming.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }


            }

            DateTime endTime = DateTime.Now;
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Start Time: {startTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"End Time: {endTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Elapsed time: {elapsed.TotalSeconds} seconds");
            // Keep the console window open
            Console.ReadLine();
        }

        static async Task<string> GetApiResponseAsync(HttpClient client, string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response from {url}:");
                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} from {url}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message} from {url}");
                return null;
            }
        }
    }
}
