using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MomPosApi.Repositories
{
    public class ConnectionRepo:IConnectionRepo
    {
        private readonly SqlConnection _connection;
        public ConnectionRepo(IConfiguration config){
            _connection = new SqlConnection(config.GetConnectionString("MomPosContext"));
        } 
        public SqlConnection ConnectDb(){
            return _connection;
        }
    }
}