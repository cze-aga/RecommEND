using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace REcommEND.Tests
{
    [TestClass]
    public class IMDBApiClientTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var apiKey = Environment.GetEnvironmentVariable("IMDBAPIKEY", EnvironmentVariableTarget.User);
            var baseAddress = "http://www.omdbapi.com/?apikey=" + apiKey + "&";

            using var client = new HttpClient();

            client.BaseAddress = new Uri(baseAddress);

            var url = "t=Memento";
            HttpResponseMessage response = client.GetAsync(baseAddress + url).Result;

            var resp = response.Content.ReadAsStringAsync();


        }
    }
}
