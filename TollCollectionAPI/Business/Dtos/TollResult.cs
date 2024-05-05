using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class TollResult
    {
        public string VehicleRegistrationNumber { get; set; }

        public decimal TotalTaxAmount { get; set; }
    }
}
