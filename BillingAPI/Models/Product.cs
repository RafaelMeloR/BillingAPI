﻿namespace BillingAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal productUniquePrice { get; set; }
        public int serviceId { get; set; }
    }
}
