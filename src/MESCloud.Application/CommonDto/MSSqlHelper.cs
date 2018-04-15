using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.CommonDto
{
    public class MSSqlHelper
    {
        /// <summary>
        ///初始化MySqlHelper实例
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public MSSqlHelper(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("ErpCon");
            var connection = new SqlConnection(ConnectionString);
            Connection = connection;
            connection.Open();
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }


        public SqlConnection Connection { get; set; }
    }
}
