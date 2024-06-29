using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using System.Data;

namespace Project_Keystone.Infrastructure.Data
{
    public class ProjectKeystoneDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public ProjectKeystoneDbContext(DbContextOptions<ProjectKeystoneDbContext> options) : base(options)
        {
        }


        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
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
            var adminUserId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<User>();
            var userRoleId = "7ec4273a-4767-4b83-b385-ee2136aa2eaf";
            var adminRoleId = "dda0e414-944b-4c35-804b-4e4784abc301";
            var roles = new List<IdentityRole<string>>
            {
                new IdentityRole<string>
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole<string>
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };

            var categories = new List<Category>
            {
            new Category { CategoryId = 1, Name = "PC" },
            new Category { CategoryId = 2, Name = "PSN" },
            new Category { CategoryId = 3, Name = "Xbox" },
            new Category { CategoryId = 4, Name = "Nintendo" }
            };

            modelBuilder.Entity<Category>().HasData(categories);

            var genres = new List<Genre>
        {
            new Genre { GenreId = 1, Name = "Action" },
            new Genre { GenreId = 2, Name = "Adventure" },
            new Genre { GenreId = 3, Name = "Singleplayer" },
            new Genre { GenreId = 4, Name = "Strategy" },
            new Genre { GenreId = 5, Name = "Multiplayer" }
        };

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminUserId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "AdminPassword123!"),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Firstname = "Admin",
                Lastname = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });

            modelBuilder.Entity<Genre>().HasData(genres);

            modelBuilder.Entity<IdentityRole<string>>().HasData(roles);

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens", t => t.ExcludeFromMigrations());


            modelBuilder.Entity<User>(entity =>
            {

                entity.ToTable("USERS");
                entity.HasIndex(e => e.Lastname, "IX_LASTNAME");
                entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();
                entity.Property(e => e.Firstname).HasColumnName("FIRSTNAME");
                entity.Property(e => e.Lastname).HasColumnName("LASTNAME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.PasswordHash).HasMaxLength(512).HasColumnName("PASSWORD_HASH");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity<IdentityRole<string>>(entity =>
            {

                entity.ToTable("ROLES");

            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("USER_ROLES");

            });




            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("BASKETS");
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.Property(e => e.BasketId).HasColumnName("BASKET_ID");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(d => d.User).WithOne(p => p.Basket).HasForeignKey<Basket>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("NAME").IsRequired();
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
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
                entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.Total).HasPrecision(18, 2);
                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasForeignKey(d => d.OrderId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");
                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
                entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("NAME");
                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
                entity.Property(e => e.Price).HasColumnName("PRICE");
                entity.Property(e => e.ImageUrl).HasColumnName("IMAGE_URL").HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("WISHLISTS");
                entity.Property(e => e.WishlistId).HasColumnName("WISHLIST_ID");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.HasOne(d => d.User).WithOne(p => p.Wishlist).HasForeignKey<Wishlist>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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

                entity.HasOne(pg => pg.Product)
                    .WithMany(p => p.ProductGenres)
                    .HasForeignKey(pg => pg.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasOne(pg => pg.Genre)
                    .WithMany(g => g.ProductGenres)
                    .HasForeignKey(pg => pg.GenreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });


            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(pc => new { pc.ProductId, pc.CategoryId });

                entity.HasOne(pc => pc.Product)
                      .WithMany(p => p.ProductCategories)
                      .HasForeignKey(pc => pc.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

                entity.HasOne(pc => pc.Category)
                      .WithMany(c => c.ProductCategories)
                      .HasForeignKey(pc => pc.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();


                entity.ToTable("PRODUCT_CATEGORIES");
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
            
            
   


