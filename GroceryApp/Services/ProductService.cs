using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroceryApp.Domain;

using GroceryApp.Tools;
using System.Data;
using GroceryApp.Request;
using System.Data.SqlClient;
using GroceryApp.Adapter;
using GroceryApp.Response;

namespace GroceryApp.Services
{
    public class ProductService : BaseService, IProductService
    {

        public int ProductInsert(ProductAddRequest model)
        {

            int id = 0;

            DbCmdDef cmdDef = new DbCmdDef
            {
                DbCommandText = "dbo.Product_Insert",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                {
                    SqlDbParameter.Instance.BuildParameter("@ProductName", model.ProductName, System.Data.SqlDbType.NVarChar, 100),
                    SqlDbParameter.Instance.BuildParameter("@Price", model.Price, System.Data.SqlDbType.Decimal),
                    SqlDbParameter.Instance.BuildParameter("@StoreId", model.StoreId, System.Data.SqlDbType.Int),
                    SqlDbParameter.Instance.BuildParameter("@Size", model.Size, System.Data.SqlDbType.Decimal),
                    SqlDbParameter.Instance.BuildParameter("@Brand", model.Brand, System.Data.SqlDbType.NVarChar, 50),
                    SqlDbParameter.Instance.BuildParameter("@Id", id, SqlDbType.Int, paramDirection: ParameterDirection.Output)

                }

            };
            Adapter.ExecuteQuery(cmdDef, (collection =>
            {
                int.TryParse(collection["@Id"].ToString(), out id);

            }));
            return id;

        }

        public void ProductUpdate(ProductUpdateRequest model)
        {
            Adapter.ExecuteQuery(new DbCmdDef
            {
                DbCommandText = "dbo.Product_Update",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                 {   SqlDbParameter.Instance.BuildParameter("@Id", model.Id, System.Data.SqlDbType.Int),
                    SqlDbParameter.Instance.BuildParameter("@ProductName", model.ProductName, System.Data.SqlDbType.NVarChar, 100),
                    SqlDbParameter.Instance.BuildParameter("@Price", model.Price, System.Data.SqlDbType.Decimal),
                    SqlDbParameter.Instance.BuildParameter("@StoreId", model.StoreId, System.Data.SqlDbType.Int),
                    SqlDbParameter.Instance.BuildParameter("@Size", model.Size, System.Data.SqlDbType.Decimal),
                    SqlDbParameter.Instance.BuildParameter("@Brand", model.Brand, System.Data.SqlDbType.NVarChar, 50),


                }
            });
        }

        public void ProductDelete(int id)
        {
            DbCmdDef cmd = new DbCmdDef
            {
                DbCommandText = "dbo.Product_DeleteById",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                {
                    SqlDbParameter.Instance.BuildParameter("@Id", id, System.Data.SqlDbType.Int)
                }
            };
            Adapter.ExecuteQuery(cmd);

        }

        public IEnumerable<Product> GetByProductKeyword(string keyword)
        {
          

            DbCmdDef cmd = new DbCmdDef
            {
                DbCommandText = "dbo.Product_SelectByProductName",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                {
                    SqlDbParameter.Instance.BuildParameter("@Keyword", keyword, System.Data.SqlDbType.NVarChar, 100)
                }
            };

           return Adapter.LoadObject<Product>(cmd);
            

        }
    }
}