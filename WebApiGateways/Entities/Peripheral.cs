using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGateways.Entities
{
    public class Peripheral
    {
        //Peripheral devices Id or UID
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UID { get; set; }
        //Device vendor
        public string Vendor { get; set; }
        //Device creation date
        public DateTime Date { get; set; }
        //Status
        public bool Status { get; set; }
    }
}
