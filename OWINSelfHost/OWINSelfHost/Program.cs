using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using OWINSelfHost.Api;


namespace OWINSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:54321/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server started...");

                //var client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/v1").Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result); 
                Console.ReadKey();
            }
        }
    }
}
