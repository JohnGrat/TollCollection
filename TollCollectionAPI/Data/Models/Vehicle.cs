using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    [PrimaryKey(nameof(RegistrationNumber), nameof(Timestamp))]
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }

        public DateTime Timestamp { get; set; }

        public int VehicleTypeId { get; set; }

        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }
    }
}
