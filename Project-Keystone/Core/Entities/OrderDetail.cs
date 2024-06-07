namespace Project_Keystone.Core.Entities
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }
    }
}
