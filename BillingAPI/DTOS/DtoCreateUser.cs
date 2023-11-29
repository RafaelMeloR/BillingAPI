namespace BillingAPI.DTOS
{
    public class DtoCreateUser
    {
        public string password { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public bool userType { get; set; }

        //Address 
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }

    }
}
