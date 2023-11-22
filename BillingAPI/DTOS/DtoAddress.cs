namespace BillingAPI.DTOS
{
    public class DtoAddress
    {
        public int id { get; set; }
        public string address { get; set; } 
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public bool status { get; set; }
    }
}
