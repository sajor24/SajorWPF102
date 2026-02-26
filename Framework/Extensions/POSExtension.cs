using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Domain.Models;

namespace Framework.Extensions
{
    public class POSExtension
    {
        public static DynamicParameters ToPOSDynamicParameters(this POSModel model)
        {
            var param = new DynamicParameters();
            param.Add("@ProductID", model.ProductID, DbType.Int32, ParameterDirection.Input);
            param.Add("@ProductName", model.ProductName, DbType.String, ParameterDirection.Input);
            param.Add("@Price", model.Price, DbType.String, ParameterDirection.Input);
            param.Add("@Stock", model.Stock, DbType.Int32, ParameterDirection.Input);
            
            return param;
        }


    }
}

