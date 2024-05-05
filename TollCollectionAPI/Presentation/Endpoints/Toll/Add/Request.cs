namespace Presentation.Endpoints.Toll.Add
{
    public class Request
    {
        public string RegistrationNumber { get; set; }

        public DateTime Timestamp { get; set; }

        public string VehicleTypeName { get; set; }
    }
}
