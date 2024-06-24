using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MomPosApi.Repositories{

    public interface IConnectionRepo
    {
        public SqlConnection ConnectDb();        
    }
}
