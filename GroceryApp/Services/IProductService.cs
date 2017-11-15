using GroceryApp.Domain;
using GroceryApp.Request;
using System.Collections.Generic;

namespace GroceryApp.Services
{
    public interface IProductService
    {
        int ProductInsert(ProductAddRequest model);
        void ProductUpdate(ProductUpdateRequest model);
        void ProductDelete(int id);
        IEnumerable<Product> GetByProductKeyword(string keyword);
    }
}