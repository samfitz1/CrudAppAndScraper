using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryApp.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int StoreId { get; set; }
        public double Size { get; set; }
        public string Brand { get; set; }

    }
}