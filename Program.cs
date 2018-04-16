using System;
using System.Threading.Tasks;
using RestSharp;

namespace myAsyncApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            GreetAsync().ConfigureAwait(false);
            Console.WriteLine("Im some code in between!");
            
            GetGreetingsAsync1();
            Console.WriteLine("Something after GetGreetingsAsync1!");
            Console.ReadLine();
        }
        
        private static async Task GreetAsync()
        {
            await Task.Run(() => GetGreetingsAsync2());
            Console.WriteLine("Something after GetGreetingsAsync2!");
            Console.WriteLine("Async Application Started!");
            //Call Async method
            var greetMsg = GetGreetingsAsync();

            //Do your other stuffs synchronously while async method is in progress            
            Console.WriteLine("Async Method in started....");

            Console.WriteLine("Current Time: " + DateTime.Now);
            Console.WriteLine("Awaiting result from Async method...");

            //All work completed, wait for async method to complete
            string msg = await greetMsg;

            //Print Async Method Result
            Console.WriteLine("Async method completed!");
            Console.WriteLine("Current Time: " + DateTime.Now);
            Console.WriteLine("Async method output: " + greetMsg.Result);

            Console.WriteLine("Async Application Ended!");
            Console.Read();
        }
        
        private static async Task<string> GetGreetingsAsync()
        {
            //simulate long running process
            await Task.Delay(10000);
            return "Welcome to Async C# Demo!";
        }
        
        private static async void GetGreetingsAsync1()
        {
            //simulate long running process
            await Task.Delay(100);
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("/comments", Method.GET);
            client.ExecuteAsync(request, response => {
                Console.WriteLine("Response is back!");
            });
        }
        
        private static async void GetGreetingsAsync2()
        {
            //simulate long running process
            await Task.Delay(100);
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("/comments", Method.GET);
            client.ExecuteAsync(request, response => {
                Console.WriteLine("Response is back of async2!");
            });
        }
    }
}
