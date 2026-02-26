using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Commands
{
    public interface IDeletePOS
    {
        
        
            Task ExecuteAsync(POSModel model);
        
    }
}
