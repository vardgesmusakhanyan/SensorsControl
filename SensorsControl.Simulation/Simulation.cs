using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Simulation
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .BuildServiceProvider();

        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        var httpClient = httpClientFactory!.CreateClient();

        List<int> deviceIds = new List<int> { 1, 2, 3 };
        TimeSpan measurementInterval = TimeSpan.FromHours(1);

        while (true)
        {
            foreach (var deviceId in deviceIds)
            {
                string apiUrl = "http://localhost:5220/devices/" + deviceId.ToString() + "/telemetry";

                await SendPostRequest(httpClient, apiUrl, deviceId.ToString());
            }
            await Task.Delay(measurementInterval);
        }
    }

    static async Task SendPostRequest(HttpClient httpClient, string apiUrl, string deviceId)
    {

        var measurements = new List<object>();
        var minVal = 0.0;
        var utcNow = DateTimeOffset.UtcNow;

        for (int i = 0; i < 4; i++)
        {
            var random = new Random();
            var illum = GenerateValue(minVal);
            var measurement = new
            {
                illum,
                time = utcNow.ToUnixTimeSeconds()
            };
            measurements.Add(measurement);
            utcNow = utcNow.AddMinutes(15);
            minVal = illum;
        }

        var requestData = JsonConvert.SerializeObject(measurements);
        var content = new StringContent(requestData, Encoding.UTF8, "application/json");

        var requestUrl = apiUrl;

        HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"POST Request sent successfully for device: {deviceId}");
        }
        else
        {
            Console.WriteLine($"POST Request failed for device: {deviceId}, Status code: {response.StatusCode}");
        }
    }

    static double GenerateValue(double minVal)
    {
        Random random = new Random();
        DateTime noon = DateTime.Today.AddHours(12);
        double maxVal = GenerateRandomNumber(random, 200, 400);
        var val = minVal == 0 ? maxVal : minVal;

        if (DateTimeOffset.UtcNow < noon)
        {
            val += random.NextDouble() * 10;
            val = Math.Min(val, maxVal);
        }

        else
        {
            val -= random.NextDouble() * 10;
            val = Math.Max(val, 0);
        }

        return Math.Ceiling((val) / 0.5) * 0.5;
    }

    static double GenerateRandomNumber(Random random, double minVal, double maxVal)
    {
        return random.NextDouble() * (maxVal - minVal) + minVal;
    }
}

