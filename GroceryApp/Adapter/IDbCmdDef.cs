using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryApp.Adapter
{
    public interface IDbCmdDef
    {
        string DbCommandText { get; set; }

        CommandType DbCommandType { get; set; }

        IDbDataParameter[] DbParameters { get; set; }

    }
}
