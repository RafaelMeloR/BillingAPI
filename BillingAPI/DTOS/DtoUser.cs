using System.ComponentModel.DataAnnotations;

namespace BillingAPI.DTOS
{
    public class DtoUser
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
        public bool userType { get; set; }
        public bool status { get; set; }

        //Address 
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
