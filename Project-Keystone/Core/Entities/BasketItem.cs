namespace Project_Keystone.Core.Entities
{
    public class BasketItem
    {
        public int BasketItemId { get; set; }
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } =  DateTime.UtcNow;
    }
}
