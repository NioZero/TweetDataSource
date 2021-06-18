using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweetinvi.Parameters.Enum;

namespace TweetScanner
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            // or you simply initialize the bearer token of client
            var consumerOnlyCredentials = new ConsumerOnlyCredentials("CONSUMER_KEY", "CONSUMER_SECRET");
            var appClient = new TwitterClient(consumerOnlyCredentials);

            await appClient.Auth.InitializeClientBearerTokenAsync();

            // complex search
            var tweets = await appClient.Search.SearchTweetsAsync(new SearchTweetsParameters("cuando")
            {
                Lang = LanguageFilter.Spanish,
                Filters = TweetSearchFilters.Images | TweetSearchFilters.Twimg,
            });


            Console.ReadLine();

            await host.RunAsync();

            return 0;
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);
    }
}
