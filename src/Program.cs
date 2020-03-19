using System;
using Polly;
using System.Threading.Tasks;
using System.Net.Http;
namespace PollyRetryConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var badurl = "https://jsonplaceholder.typicode2.com/todos/1";
            var url = "https://jsonplaceholder.typicode.com/todos/1";
            await new RetryPolicy().retryPolicy(badurl);
            await new waitAndRetryPolicy().WaitAndRetry(badurl);

        }

       

    }
}
