using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [PrimaryKey(nameof(RegistrationNumber), nameof(Timestamp))]
    public class TollPassage
    {
        public string RegistrationNumber { get; set; }

        public DateTime Timestamp { get; set; }

        public int VehicleTypeId { get; set; }

        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }
    }
}
