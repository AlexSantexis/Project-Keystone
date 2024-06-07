namespace Project_Keystone.Core.Entities
{
    public class ProductGenre
    {
        public int ProductId { get; set; }
        public Product product { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre genre { get; set; } = null!;
    }
}
