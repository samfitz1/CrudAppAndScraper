using GroceryApp.Adapter;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GroceryApp.Services
{
    public class BaseService
    {

        public IDbAdapter Adapter
        {
            get
            {
                return new DbAdapter(new SqlCommand(), new SqlConnection("Server=LAPTOP-9V28LNKS\\SQLEXPRESS01; DataBase=GroceryApp; Trusted_Connection=True;"));
            }
        }

    }
}