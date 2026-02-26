using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Domain.Models;
namespace Framework.Extensions
{
    public static class EmployeeExtension
    {
        public static DynamicParameters ToEmployeeDynamicParameters(this EmployeeModel model)
        {
            var param = new DynamicParameters();
            param.Add("@EmployeeId", model.EmployeeId, DbType.Int32, ParameterDirection.Input);
            param.Add("@FirstName", model.FirstName, DbType.String, ParameterDirection.Input);
            param.Add("@LastName", model.LastName, DbType.String, ParameterDirection.Input);
            param.Add("@Age", model.Age, DbType.Int32, ParameterDirection.Input);
            param.Add("@Position", model.Position, DbType.String, ParameterDirection.Input);
            return param;
        }

        public static DynamicParameters ToCreateEmployeeDynamicParameters(this EmployeeModel model)
        {
            var param = new DynamicParameters();
            param.Add("@FirstName", model.FirstName, DbType.String, ParameterDirection.Input);
            param.Add("@LastName", model.LastName, DbType.String, ParameterDirection.Input);
            param.Add("@Age", model.Age, DbType.Int32, ParameterDirection.Input);
            param.Add("@Position", model.Position, DbType.String, ParameterDirection.Input);
            return param;
        }

        public static DynamicParameters ToDeleteEmployeeDynamicParameters(this EmployeeModel model)
        {
            var param = new DynamicParameters();
            param.Add("@EmployeeId", model.EmployeeId, DbType.Int32, ParameterDirection.Input);
            return param;
        }

    }
}
