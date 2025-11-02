namespace AirBB.Models
{
    public class Filter
    {
        public Filter(string filters)
        {
            filter = filters ?? "all-all-all-all";
            string[] filteredSplit = filter.Split('-');
            LocationID = filteredSplit[0];
            CheckInDateID = filteredSplit[1];
            CheckOutDateID = filteredSplit[2];
            NoOfGuestsID = filteredSplit[3];
        }
        public string filter { get; }
        public string LocationID { get; }
        public string CheckInDateID { get; }
        public string CheckOutDateID { get; }
        public string NoOfGuestsID { get; }
    }
}
