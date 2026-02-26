using Domain.Commands;
using Domain.Models;   
using Framework.Extensions;

namespace Framework.Commands
{
    public class UpdatePOS : IUpdatePOS
    {
        private readonly Repository _repository;

        public UpdatePOS(Repository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(POSModel model)
        {
            var parameters = model.ToPOSDynamicParameters();
            await _repository.SaveDataAsync("DefaultConnection", "[dbo].[UpdatePOS]", parameters);
        }
    }
}
