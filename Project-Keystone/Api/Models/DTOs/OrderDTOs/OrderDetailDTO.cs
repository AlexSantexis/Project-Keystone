﻿namespace Project_Keystone.Api.Models.DTOs.OrderDTOs
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
