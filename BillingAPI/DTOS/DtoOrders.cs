﻿namespace BillingAPI.DTOS
{
    public class DtoOrders
    {
        public int Id { get; set; }
        public DateTime creationDate { get; set; }
        public int userId { get; set; }
        public DateTime updatedDate { get; set; }
        public bool status { get; set; }
        public string accountNumber { get; set; }
    }
}
