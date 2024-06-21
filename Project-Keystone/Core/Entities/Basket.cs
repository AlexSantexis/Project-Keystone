namespace Project_Keystone.Core.Entities
{
    public class Basket
    {
        public int BasketId { get; set; }

        public string? UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<BasketItem> BasketItems { get; set; } = new HashSet<BasketItem>();
    }
}
