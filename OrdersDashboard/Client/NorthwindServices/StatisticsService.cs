using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OrdersDashboard.Shared.Model;
using System.IO;

using System.Net.Http.Json;

namespace OrdersDashboard.Client.NorthwindServices
{
    public class StatisticsService
    {
        private string address = "https://northwind.now.sh/api/orders";

        HttpClient _httpClient;
        HttpRequestMessage request;
        HttpResponseMessage response;
        List<OrdersByCountry> ordersByCountries;
        List<Order> orders;
        public StatisticsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<(List<OrdersByCountry> Data, int StatusCode)> GetOrdersByCountriesAsync()
        {
            int statusCode;
            request = new HttpRequestMessage()
            {
                RequestUri = new Uri(address),
                Method = HttpMethod.Get
            };
            request.Headers.Add("mode", "no-cors");

            try
            {
                response = await _httpClient.SendAsync(request);

                string json = await response.Content.ReadAsStringAsync();

                orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json).ToList();


                var groupedOrders = orders.GroupBy(order => order.ShipAddress.Country).ToList();

                ordersByCountries = (List<OrdersByCountry>)groupedOrders.Select(orderGroup =>
                new OrdersByCountry
                {
                    CountryName = orderGroup.Key,
                    OrdersCount = orderGroup.Count()
                }).OrderByDescending(ordersByCountry => ordersByCountry.OrdersCount).Take(10).ToList();

                statusCode = 200;
            }
            catch (NotSupportedException)
            {
                ordersByCountries = null;
                statusCode = 500;
            }

            catch (HttpRequestException)
            {
                ordersByCountries = null;
                statusCode = 500;
            }

            return (ordersByCountries, statusCode);
        }
    }

    public class OrdersByCountry
    {
        public string CountryName { set; get; }
        public int OrdersCount { set; get; }
    }
}

//{
//    "id": 10271,
//    "customerId": "SPLIR",
//    "employeeId": 6,
//    "orderDate": "1996-08-01 00:00:00.000",
//    "requiredDate": "1996-08-29 00:00:00.000",
//    "shippedDate": "1996-08-30 00:00:00.000",
//    "shipVia": 2,
//    "freight": 4.54,
//    "shipName": "Split Rail Beer & Ale",
//    "shipAddress": {
//        "street": "P.O. Box 555",
//      "city": "Lander",
//      "region": "WY",
//      "postalCode": 82520,
//      "country": "USA"
//    },
//    "details": [
//      {
//        "productId": 33,
//        "unitPrice": 2,
//        "quantity": 24,
//        "discount": 0
//      }
//    ]
//  }
