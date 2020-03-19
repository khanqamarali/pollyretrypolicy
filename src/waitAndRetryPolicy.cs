using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PollyRetryConsoleApp
{
   public class waitAndRetryPolicy
    {
        public async Task WaitAndRetry(string url)
        {   
            var httpClient = new HttpClient();
            int maxRetryAttempts = 3;
            int waitTimespan = 2;
            TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(10);
            var httpResponse = await Policy
                               .Handle<Exception>()
                                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                        .WaitAndRetryAsync(maxRetryAttempts, i => TimeSpan.FromSeconds(waitTimespan), (result, timeSpan, retryCount, context) =>
                        {
                            Console.WriteLine($"Request failed Waiting {timeSpan} before next retry. Retry attempt {retryCount}");           
                        }).ExecuteAsync(async () => await httpClient.GetAsync(url));
            if (httpResponse.IsSuccessStatusCode)
                Console.WriteLine("Response was successful.");
            else
                Console.WriteLine($"Response failed. Status code {httpResponse.StatusCode}");
        }
    }
}
