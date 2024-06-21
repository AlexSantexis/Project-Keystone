namespace Project_Keystone.Core.Entities
{
    public class ProductGenre
    {
        public int ProductId { get; set; }
        public virtual Product product { get; set; } = null!;
        public int GenreId { get; set; }
        public virtual Genre genre { get; set; } = null!;
    }
}
