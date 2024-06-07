namespace Project_Keystone.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
