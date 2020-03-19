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
            TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(10);
            var retryPolicy = Policy
                               .Handle<Exception>()
                                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                                .WaitAndRetryAsync(maxRetryAttempts,(ex, retrycnt) =>
                                {
                                    Console.WriteLine("Retrying it");
                                });
            var uploadResponse = await retryPolicy.ExecuteAsync(async () =>
                await httpClient.GetAsync(url)
            );
        }
    }
}
