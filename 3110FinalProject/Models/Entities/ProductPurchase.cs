﻿namespace _3110FinalProject.Models.Entities
{
    public class ProductPurchase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int PurchaseId { get; set; }
        public Purchase? Purchase { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
