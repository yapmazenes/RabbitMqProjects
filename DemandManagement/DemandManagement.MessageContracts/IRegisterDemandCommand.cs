using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemandManagement.MessageContracts
{
    public interface IRegisterDemandCommand
    {
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
