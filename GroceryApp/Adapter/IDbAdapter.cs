using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryApp.Adapter
{
    public interface IDbAdapter
    {
        int CommandTimeOut { get; set; }
        IDbCommand DbCommand { get; }
        IDbConnection DbConnection { get; }

        int ExecuteQuery(IDbCmdDef cmdDef, Action<IDataParameterCollection> returnParameters = null);
        object ExecuteScalar(IDbCmdDef cmdDef);
        IEnumerable<T> LoadObject<T>(DbCmdDef cmdDef) where T : class;
        IEnumerable<T> LoadObject<T>(DbCmdDef cmdDef, Func<IDataReader, T> mapper) where T : class;

    }
}
