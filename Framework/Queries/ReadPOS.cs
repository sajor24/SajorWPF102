using Framework.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Queries
{
    public class ReadPOSById : IGetPOSById
    {

        private readonly Repository _repository;

        public ReadPOSById(Repository repository)
        {
            _repository = repository;
        }

        public async Task<POSModel?> ExecuteAsync(int productID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", ProductID);

            var data = await _repository.GetDataAsync<POSModel>(
                "DefaultConnection",
                "[dbo].[GetPOSById]",
                parameters
            );

            return data?.FirstOrDefault();
        }
    }
}
    

