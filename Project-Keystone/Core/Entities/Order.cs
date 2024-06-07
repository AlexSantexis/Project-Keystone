namespace Project_Keystone.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }
        public string StreetAddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string Country { get; set; } = null!;



        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}
