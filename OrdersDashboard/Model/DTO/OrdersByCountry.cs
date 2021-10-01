using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDashboard.Model.DTO
{
    public class OrdersByCountry
    {
        public string CountryName { set; get; }
        public int OrdersCount { set; get; }
    }
}
