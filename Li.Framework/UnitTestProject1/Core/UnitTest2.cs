using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using SqlSugar;

namespace UnitTestProject1.Core
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var connectionStr = ConfigurationManager.ConnectionStrings["MessageDb"].ConnectionString;

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionStr, //必填
                DbType = DbType.SqlServer, //必填
                IsAutoCloseConnection = true, //默认false
                InitKeyType = InitKeyType.Attribute //默认SystemTable
            });


        }
    }
}
