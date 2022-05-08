using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.MessageContracts.Events
{
    public interface IProductEvent
    {
        string ProductName { get; set; }
        int Quantity { get; set; }
    }
}
