using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryApp.Request
{
    public class ProductUpdateRequest: ProductAddRequest
    {
        public int Id { get; set; }
    }
}