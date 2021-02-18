using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGateways.Entities
{
    public class PeripheralsGateways
    {
        public string GatewaySerialNumber { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PeripheralId { get; set; }
    }
}
