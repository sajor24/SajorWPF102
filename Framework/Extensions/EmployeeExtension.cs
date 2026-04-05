using System.Data;
using Dapper;
using Domain.Models;

namespace Framework.Extensions
{
    public static class EmployeeExtension
    {
        // All fields — used for Update
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

        // No EmployeeId — used for Create (IDENTITY column)
        public static DynamicParameters ToCreateEmployeeDynamicParameters(this EmployeeModel model)
        {
            var param = new DynamicParameters();
            param.Add("@FirstName", model.FirstName, DbType.String, ParameterDirection.Input);
            param.Add("@LastName", model.LastName, DbType.String, ParameterDirection.Input);
            param.Add("@Age", model.Age, DbType.Int32, ParameterDirection.Input);
            param.Add("@Position", model.Position, DbType.String, ParameterDirection.Input);
            return param;
        }

        // Only EmployeeId — used for Delete
        public static DynamicParameters ToDeleteEmployeeDynamicParameters(this EmployeeModel model)
        {
            var param = new DynamicParameters();
            param.Add("@EmployeeId", model.EmployeeId, DbType.Int32, ParameterDirection.Input);
            return param;
        }
    }
}
