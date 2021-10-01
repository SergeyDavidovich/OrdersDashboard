using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDashboard.Model
{
   public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }
    }
}

//"id": 4,
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
//    "id": 2,
//        "companyName": "New Orleans Cajun Delights",
//        "contactName": "Shelley Burke",
//        "contactTitle": "Order Administrator",
//        "address": {
//        "street": "P.O. Box 78934",
//            "city": "New Orleans",
//            "region": "LA",
//            "postalCode": 70117,
//            "country": "USA",
//            "phone": "(100) 555-4822"
//        }
//},
//    "category": {
//    "id": 3,
//        "description": "Desserts candies and sweet breads",
//        "name": "Confections"
//    }
