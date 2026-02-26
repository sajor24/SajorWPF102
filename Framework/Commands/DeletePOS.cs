using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Commands; 
using Domain.Models;

namespace Framework.Commands 
{
    public class DeletePOS : IDeletePOS
    {
        private readonly Repository _repository;

        public CreatePOS(Repository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(POSModel model)
        {
            var parameters = model.ToPOSDynamicParameters();
            await _repository.SaveDataAsync("DefaultConnection", "[dbo].[CreatePOS]", parameters);
        }
    }
}

