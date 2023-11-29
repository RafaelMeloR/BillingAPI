namespace BillingAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string password { get; set; } 
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int addressId { get; set; }
        public string phone { get; set; }
        public string ipAddress { get; set; }
        public string macAddress { get; set; }
        public DateTime lastLogin { get; set; }
        public bool usertype { get; set; }
        public bool status { get; set; }
    }
}
