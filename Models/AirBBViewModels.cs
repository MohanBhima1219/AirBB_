namespace AirBB.Models
{
    public class AirBBViewModels
    {
        public string ActiveLocation { get; set; } = "all";
        public string ActiveCheckInDate { get; set; } = "all";
        public string ActiveCheckOutDate { get; set; } = "all";
        public string ActiveNoOfGuests { get; set; } = "all";
        public List<Location> Location { get; set; } = new List<Location>();
        public List<Reservation> Reservation { get; set; } = new List<Reservation>();
        public List<Residence> Residence { get; set; } = new List<Residence>();
        public List<Client> Client { get; set; } = new List<Client>();
        public Location Locations { get; set; } = new Location();
        public Reservation Reservations { get; set; } = new Reservation();
        public Residence Residences { get; set; } = new Residence();
        public Client Clients { get; set; } = new Client();

        public string CheckActiveLocation(string d) =>
            d.ToLower() == ActiveLocation.ToLower() ? "active" : "";
        public string CheckActiveCheckInDate(string d) =>
            d.ToLower() == ActiveCheckInDate.ToLower() ? "active" : "";
        public string CheckActiveCheckOutDate(string d) =>
            d.ToLower() == ActiveCheckOutDate.ToLower() ? "active" : "";
        public string CheckActiveNoOfGuests(string d) =>
            d.ToLower() == ActiveNoOfGuests.ToLower() ? "active" : "";
    }
}
