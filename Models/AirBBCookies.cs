namespace AirBB.Models
{
    public class AirBBCookies
    {
        private const string ReservationKey = "myReservations";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; } = null!;
        private IResponseCookies responseCookies { get; set; } = null!;

        public AirBBCookies(IRequestCookieCollection request, IResponseCookies response)
        {
            requestCookies = request;
            responseCookies = response;
        }
        public void RemoveReservationId(int id)
        {
            string[] ids = GetMyReservationIds();
            var updatedIds = ids.Where(rid => rid != id.ToString()).ToArray();
            SetMyReservationIds(updatedIds);
        }
        public void SetMyReservationIds(List<Reservation> myReservations)
        {
            var ids = myReservations.Select(r => r.ReservationId.ToString()).ToList();
            SetMyReservationIds(ids);
        }
        public void SetMyReservationIds(IEnumerable<string> ids)
        {
            if (responseCookies == null)
                throw new InvalidOperationException("Response cookies are not initialized.");

            string idsString = string.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            RemoveMyReservationIds(); 
            responseCookies.Append(ReservationKey, idsString, options);
        }
        public string[] GetMyReservationIds()
        {
            string cookie = requestCookies[ReservationKey] ?? String.Empty;
            if (string.IsNullOrEmpty(cookie))
                return Array.Empty<string>(); 
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMyReservationIds()
        {
            if (responseCookies == null)
                throw new InvalidOperationException("Response cookies are not initialized.");

            responseCookies.Delete(ReservationKey);
        }
    }
}
