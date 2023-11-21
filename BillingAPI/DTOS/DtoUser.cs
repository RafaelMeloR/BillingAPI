using System.ComponentModel.DataAnnotations;

namespace BillingAPI.DTOS
{
    public class DtoUser
    {
        
        public int id { get; set; }
        [Required (ErrorMessage ="Field Required")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public int addressId { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [RegularExpression(@"^(\+\d{1, 2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        public string phone { get; set; } 
        public string ipAddress { get; set; }
        public string macAddress { get; set; }
        public DateTime lastLogin { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public bool usertype { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public bool status { get; set; }

        public string ErrorMessage { get; set; }
    }
}
