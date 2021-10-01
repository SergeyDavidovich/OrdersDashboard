using Newtonsoft.Json;
using OrdersDashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrdersDashboard.Client.NorthwindServices
{
    public class ProductsService
    {
        private readonly string url = "https://northwind.now.sh/api/products";

        HttpClient _httpClient;  
        HttpRequestMessage request;
        HttpResponseMessage response;
        List<Product> products;
        
        public ProductsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Product>> GetProductsAsync1()
        {

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("mode", "no-cors");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            List<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json).ToList();

            return products;
        }
        public async Task<(List<Product> Data, int StatusCode)> GetProductsAsync()
        {
            int statusCode;
            request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Add("mode", "no-cors");

            try
            {
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                string json = await response.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json).ToList();
                statusCode = 200;
            }
            catch (NotSupportedException)
            {
                products = null;
                statusCode = 500;
            }

            catch (HttpRequestException)
            {
                products = null;
                statusCode = 500;
            }
            return (products, statusCode);
        }
    }
}
