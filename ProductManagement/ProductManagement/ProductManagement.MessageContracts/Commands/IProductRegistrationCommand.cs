using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.MessageContracts.Commands
{
    public interface IProductRegistrationCommand
    {
        string ProductName { get; set; }
        int Quantity { get; set; }
    }
}
