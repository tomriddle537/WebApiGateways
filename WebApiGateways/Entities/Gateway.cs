using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGateways.Entities
{
    public class Gateway
    {
        //A unique serial number
        public string SerialNumber { get; set; }
        //Human-readable name
        public string Name { get; set; }
        //IPv4 address
        public string IpAddress { get; set; }
    }


}
