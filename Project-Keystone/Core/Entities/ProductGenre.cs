namespace Project_Keystone.Core.Entities
{
    public class ProductGenre
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; } = null!;
    }
}
