using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDashboard.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string CustometId { get; set; }
        public string EmployeeId { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
        public string ShipVia { get; set; }
        public double Freight { get; set; }
        public string ShipName { get; set; }
        public ShipAddress ShipAddress { get; set; }
        public Details[] Details { get; set; }
    }
    public class Details
    {
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
    public class ShipAddress
    {
        public string Street { get; set; }
        public string Sity { get; set; }
        public string Region { get; set; }
        public string Ostalcode { get; set; }
        public string Country { get; set; }
    }
}
