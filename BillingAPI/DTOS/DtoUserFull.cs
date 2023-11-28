using BillingAPI.Models;

namespace BillingAPI.DTOS
{
    public class DtoUserFull
    {
        public User user { get; set; }
        public Address address { get; set; }
        public List<DtoInvoice> invoice { get; set; }
    }
}
