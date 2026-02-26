using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
{
    internal interface IReadPOSById
    {
        Task<POSModel?> ExecuteAsync(int ProductID);
    }
}
