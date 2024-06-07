namespace Project_Keystone.Core.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ProductGenre> ProductGenres { get; set; } = new HashSet<ProductGenre>();
    }
}
