using Project_Keystone.Api.Models.DTOs.AddressDTos;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Project_Keystone.Api.Models.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public IList<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}

