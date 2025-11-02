namespace AirBB.Models
{
    public class AirBBSession
    {
        private const string ReservationKey = "myReservations";
        private const string CountKey = "reservationsCount";
        private const string ActiveLocationKey = "location";
        private const string CheckInDateKey = "checkIn";
        private const string CheckOutDateKey = "checkOut";
        private const string NoOfGuestsKey = "noOfGuests";

        private ISession session { get; set; }
        public AirBBSession(ISession session) => this.session = session;

        public void SetMyReservations(List<Reservation> reservations)
        {
            session.SetObject(ReservationKey, reservations);
            session.SetInt32(CountKey, reservations.Count);
        }
        public List<Reservation> GetMyReservations() =>
            session.GetObject<List<Reservation>>(ReservationKey) ?? new List<Reservation>();
        public int? GetMyReservationCount() => session.GetInt32(CountKey);

        public void SetActiveLocation(string activeLocation) =>
            session.SetString(ActiveLocationKey, activeLocation);
        public string GetActiveLocation() =>
            session.GetString(ActiveLocationKey) ?? string.Empty;

        public void SetActiveCheckInDate(string activeCheckInDate) =>
            session.SetString(CheckInDateKey, activeCheckInDate);
        public string GetActiveCheckInDate() =>
            session.GetString(CheckInDateKey) ?? string.Empty;

        public void SetActiveCheckOutDate(string activeCheckOutDate) =>
            session.SetString(CheckOutDateKey, activeCheckOutDate);
        public string GetActiveCheckOutDate() =>
            session.GetString(CheckOutDateKey) ?? string.Empty;

        public void SetActiveNoOfGuests(string activeNoOfGuests) =>
            session.SetString(NoOfGuestsKey, activeNoOfGuests);
        public string GetActiveNoOfGuests() =>
            session.GetString(NoOfGuestsKey) ?? string.Empty;
    }
}
