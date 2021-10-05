using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OrdersDashboard.Model;
using System.IO;

using System.Net.Http.Json;
using OrdersDashboard.Model.DTO;
using OrdersDashboard.Helpers;

namespace OrdersDashboard.Client.NorthwindServices
{
    public class StatisticsService
    {
        private string ordersUri = "https://northwind.now.sh/api/orders";
        private string productsUri = "https://northwind.now.sh/api/products";
        private string employeesUri = "https://northwind.now.sh/api/employess";
        private string customersUri = "https://northwind.now.sh/api/customers";
        private string suppliersUri = "https://northwind.now.sh/api/suppliers";
        private string categoryUri = "https://northwind.now.sh/api/categories";

        HttpClient _httpClient;
        HttpResponseMessage response;
        List<OrdersByCountry> ordersByCountries;
        List<SalesByCountry> salesByCountries;
        List<SalesByCategory> salesByCategories;

        List<Order> orders;

        public StatisticsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<(List<Order> Data, int StatusCode)> GetOrdersAsync()
        {
            int statusCode;
            var request = (new NoCorsRequestHelper()).GetRequest(ordersUri);
            try
            {
                response = await _httpClient.SendAsync(request);
                string json = await response.Content.ReadAsStringAsync();

                orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json).ToList();
                statusCode = 200;
            }
            catch (NotSupportedException)
            {
                orders = null;
                statusCode = 500;
            }
            catch (HttpRequestException)
            {
                orders = null;
                statusCode = 500;
            }
            return (orders, statusCode);
        }

        public async Task<(List<OrdersByCountry> Data, int StatusCode)> GetOrdersByCountriesAsync()
        {
            //int statusCode;
            //var request = (new NoCorsRequestHelper()).GetRequest(ordersUri);
            //try
            //{
            //    response = await _httpClient.SendAsync(request);

            //    string json = await response.Content.ReadAsStringAsync();

            //    orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json).ToList();

            //    var groupedOrders = orders.GroupBy(order => order.ShipAddress.Country).ToList();

            //    ordersByCountries = (List<OrdersByCountry>)groupedOrders.Select(orderGroup =>
            //    new OrdersByCountry
            //    {
            //        CountryName = orderGroup.Key,
            //        OrdersCount = orderGroup.Count()
            //    }).OrderByDescending(ordersByCountry => ordersByCountry.OrdersCount).Take(10).ToList();

            //    statusCode = 200;
            //}
            //catch (NotSupportedException)
            //{
            //    ordersByCountries = null;
            //    statusCode = 500;
            //}

            //catch (HttpRequestException)
            //{
            //    ordersByCountries = null;
            //    statusCode = 500;
            //}
            //return (ordersByCountries, statusCode);

            List<OrdersByCountry> list = new()
            {
                new OrdersByCountry() { CountryName= "Germany", OrdersCount=23},
                new OrdersByCountry() { CountryName = "UK", OrdersCount = 133 },
                new OrdersByCountry() { CountryName = "Sweden", OrdersCount = 67 },
                new OrdersByCountry() { CountryName = "France", OrdersCount = 86 },
                new OrdersByCountry() { CountryName = "Canada", OrdersCount = 43 },
                new OrdersByCountry() { CountryName = "USA", OrdersCount = 11 },
                new OrdersByCountry() { CountryName = "Switzerland", OrdersCount = 23 },
            }
                ;


            return (list, 200);
        }

        public async Task<(List<SalesByCountry> Data, int StatusCode)> GetSalesByCountriesAsync()
        {
            int statusCode;
            var request = (new NoCorsRequestHelper()).GetRequest(ordersUri);
            try
            {
                response = await _httpClient.SendAsync(request);
                string json = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json).ToList();
                var groupedOrders = orders.GroupBy(order => order.ShipAddress.Country).ToList();

                salesByCountries = groupedOrders.Select(groupedOrders => new SalesByCountry()
                {
                    CountryName = groupedOrders.Key,
                    CountrySum = groupedOrders.Sum(g => g.Details.Sum(g => g.UnitPrice * g.Quantity))
                }).OrderByDescending(salesBycountries => salesBycountries.CountrySum).Take(10).ToList();

                statusCode = 200;
            }
            catch (NotSupportedException)
            {
                salesByCountries = null;
                statusCode = 500;
            }

            catch (HttpRequestException)
            {
                salesByCountries = null;
                statusCode = 500;
            }
            return (salesByCountries, statusCode);
        }

        public async Task<(List<SalesByCategory> Data, int StatusCode)> GetSalesByCategoriesAsync()
        {
            int statusCode;
            var request = (new NoCorsRequestHelper()).GetRequest(ordersUri);
            try
            {
                response = await _httpClient.SendAsync(request);
                string json = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json).ToList();

                statusCode = 200;
                salesByCategories = null;
            }
            catch (NotSupportedException)
            {
                salesByCategories = null;
                statusCode = 500;
            }

            catch (HttpRequestException)
            {
                salesByCategories = null;
                statusCode = 500;
            }
            return (salesByCategories, statusCode);
        }
    }
}
//order
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

//product
//{
//    "id": 4,
//    "supplierId": 2,
//    "categoryId": 3,
//    "quantityPerUnit": "48 - 6 oz jars",
//    "unitPrice": 22,
//    "unitsInStock": 53,
//    "unitsOnOrder": 0,
//    "reorderLevel": 0,
//    "discontinued": true,
//    "name": "Chef Anton's Cajun Seasoning",
//    "supplier": {
//        "id": 2,
//      "companyName": "New Orleans Cajun Delights",
//      "contactName": "Shelley Burke",
//      "contactTitle": "Order Administrator",
//      "address": {
//            "street": "P.O. Box 78934",
//        "city": "New Orleans",
//        "region": "LA",
//        "postalCode": 70117,
//        "country": "USA",
//        "phone": "(100) 555-4822"
//      }
//    },
//    "category": {
//        "id": 3,
//      "description": "Desserts candies and sweet breads",
//      "name": "Confections"
//    }
//},

//{
//    "id": 2,
//    "lastName": "Fuller",
//    "firstName": "Andrew",
//    "title": "Vice President Sales",
//    "titleOfCourtesy": "Dr.",
//    "birthDate": "1952-02-19 00:00:00.000",
//    "hireDate": "1992-08-14 00:00:00.000",
//    "address": {
//        "street": "908 W. Capital Way",
//      "city": "Tacoma",
//      "region": "WA",
//      "postalCode": 98401,
//      "country": "USA",
//      "phone": "(206) 555-9482"
//    },
//    "notes": "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative was promoted to sales manager",
//    "reportsTo": "NULL",
//    "territoryIds": [
//      1730,
//      1833,
//      2116,
//      2139,
//      2184,
//      40222,
//      1581
//    ]
