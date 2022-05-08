using ProductManagement.MessageContracts.Commands;
using ProductManagement.MessageContracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.MessageContracts.Models
{
    public class Product : IProductRegistrationCommand, IProductEvent
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
