using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class POSModel
    {
        public int  ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public int Stock { get; set; }
        
    }
}
