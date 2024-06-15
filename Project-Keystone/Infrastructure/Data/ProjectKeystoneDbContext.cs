using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using System.Data;

namespace Project_Keystone.Infrastructure.Data
{
    public class ProjectKeystoneDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ProjectKeystoneDbContext(DbContextOptions<ProjectKeystoneDbContext> options) : base(options)
        {
        }

        
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }


        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<Wishlist> Wishlist { get; set; }
        public virtual DbSet<WishlistItem> WishlistItem { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<ProductGenre> ProductGenres { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasIndex(e => e.Lastname, "IX_LASTNAME");
                entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();
                entity.Property(e => e.Id).HasColumnName("USER_ID");
                entity.Property(e => e.Firstname).HasColumnName("FIRSTNAME");
                entity.Property(e => e.Lastname).HasColumnName("LASTNAME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.PasswordHash).HasMaxLength(512).HasColumnName("PASSWORD_HASH");              
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);
                entity.Property(e => e.NormalizedUserName).HasColumnName("NORMALIZED_USER_NAME");
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCY_STAMP");


                entity.Ignore(e => e.EmailConfirmed);
                entity.Ignore(e => e.TwoFactorEnabled);
                entity.Ignore(e => e.LockoutEnd);
                entity.Ignore(e => e.LockoutEnabled);
                entity.Ignore(e => e.PhoneNumberConfirmed);
                entity.Ignore(e => e.SecurityStamp);
                
                entity.Ignore(e => e.AccessFailedCount);
                entity.Ignore(e => e.PhoneNumber);
                
            });

            modelBuilder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable("ROLES");               
                entity.Ignore(r => r.ConcurrencyStamp);
                
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("USER_ROLES");
            });

            // Configuration for IdentityUserLogin<int>
            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
                entity.ToTable("USER_LOGINS");
            });

            // Configuration for IdentityUserToken<int>
            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
                entity.ToTable("USER_TOKENS");
            });

            // Configuration for IdentityUserClaim<int>
            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.HasKey(claim => claim.Id);
                entity.ToTable("USER_CLAIMS");
            });

            // Configuration for IdentityRoleClaim<int>
            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.HasKey(claim => claim.Id);
                entity.ToTable("ROLE_CLAIMS");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("BASKETS");
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.Property(e => e.BasketId).HasColumnName("BASKET_ID");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(d => d.User).WithOne(p => p.Basket).HasForeignKey<Basket>(d => d.UserId);
            });

            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.ToTable("BASKET_ITEMS");
                entity.HasIndex(e => e.BasketId);
                entity.HasIndex(e => e.ProductId);
                entity.Property(e => e.BasketItemId).HasColumnName("BASKET_ITEM_ID");
                entity.Property(e => e.BasketId).HasColumnName("BASKET_ID");
                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
                entity.Property(e => e.AddedAt).HasColumnName("ADDED_AT");
                entity.HasOne(d => d.Basket).WithMany(p => p.BasketItems).HasForeignKey(d => d.BasketId);
                entity.HasOne(d => d.Product).WithMany(p => p.BasketItems).HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORIES");
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("NAME");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");
                entity.HasIndex(e => e.UserId);
                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.OrderDate).HasColumnName("ORDER_DATE");
                entity.Property(e => e.TotalAmount).HasColumnName("TOTAL_AMOUNT");
                entity.Property(e => e.StreetAddress).HasColumnName("STREET_ADDRESS");
                entity.Property(e => e.City).HasColumnName("CITY");               
                entity.Property(e => e.ZipCode).HasColumnName("ZIP_CODE");
                entity.Property(e => e.Country).HasColumnName("COUNTRY");
                entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("ORDER_DETAILS");
                entity.Property(e => e.OrderDetailID).HasColumnName("ORDER_DETAIL_ID");
                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
                entity.Property(e => e.Price).HasColumnName("PRICE");
                entity.Property(e => e.Total).HasColumnName("TOTAL");
                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasForeignKey(d => d.OrderId);
                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");
                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
                entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("NAME");
                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
                entity.Property(e => e.Price).HasColumnName("PRICE");
                entity.Property(e => e.ImageUrl).HasColumnName("IMAGE_URL");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("WISHLISTS");
                entity.Property(e => e.WishlistId).HasColumnName("WISHLIST_ID");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(d => d.User).WithOne(p => p.Wishlist).HasForeignKey<Wishlist>(d => d.UserId);
            });

            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.ToTable("WISHLIST_ITEMS");
                entity.Property(e => e.WishlistItemId).HasColumnName("WISHLIST_ITEM_ID");
                entity.Property(e => e.WishlistId).HasColumnName("WISHLIST_ID");
                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
                entity.Property(e => e.AddedAt).HasColumnName("ADDED_AT");
                entity.HasOne(d => d.Wishlist).WithMany(p => p.WishListItems).HasForeignKey(d => d.WishlistId);
                entity.HasOne(d => d.Product).WithMany(p => p.WishlistItems).HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<ProductGenre>(entity =>
            {
                entity.ToTable("PRODUCT_GENRES");
                entity.HasKey(pg => new { pg.ProductId, pg.GenreId });

                entity.HasIndex(pg => pg.GenreId);

                entity.HasOne(pg => pg.product)
                    .WithMany(p => p.ProductGenres)
                    .HasForeignKey(pg => pg.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasOne(pg => pg.genre)
                    .WithMany(g => g.ProductGenres)
                    .HasForeignKey(pg => pg.GenreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("GENRES");
                entity.Property(e => e.GenreId).HasColumnName("GENRE_ID");
                entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("NAME");

                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESSES");
                entity.HasKey(a => a.AddressId);
                entity.Property(a => a.AddressId).HasColumnName("ADDRESS_ID");
                entity.Property(a => a.StreetAddress).HasColumnName("STREET_ADDRESS");
                entity.Property(a => a.City).HasColumnName("CITY");
                entity.Property(a => a.ZipCode).HasColumnName("ZIP_CODE");
                entity.Property(a => a.Country).HasColumnName("COUNTRY");
                entity.Property(a => a.UserId).HasColumnName("USER_ID");
                entity.HasOne(a => a.User)
                    .WithOne(u => u.Address)
                    .HasForeignKey<Address>(a => a.UserId);
            });

        }
    }
}
