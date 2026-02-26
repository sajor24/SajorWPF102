using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Framework.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Commands
{
    internal class CreatePOS : ICreatePOS
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

