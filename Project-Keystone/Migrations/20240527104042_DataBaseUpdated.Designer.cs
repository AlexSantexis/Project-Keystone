﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Keystone.Infrastructure.Data;

#nullable disable

namespace Project_Keystone.Migrations
{
    [DbContext(typeof(ProjectKeystoneDbContext))]
    [Migration("20240527104042_DataBaseUpdated")]
    partial class DataBaseUpdated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project_Keystone.Core.Entities.Basket", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BASKET_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BasketId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.HasKey("BasketId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("BASKETS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.BasketItem", b =>
                {
                    b.Property<int>("BasketItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BASKET_ITEM_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BasketItemId"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ADDED_AT");

                    b.Property<int>("BasketId")
                        .HasColumnType("int")
                        .HasColumnName("BASKET_ID");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCT_ID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTITY");

                    b.HasKey("BasketItemId");

                    b.HasIndex("BasketId");

                    b.HasIndex("ProductId");

                    b.ToTable("BASKET_ITEMS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CATEGORY_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NAME");

                    b.HasKey("CategoryId");

                    b.HasIndex("Name");

                    b.ToTable("CATEGORIES", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ORDER_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ORDER_DATE");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SHIPPING_ADDRESS");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TOTAL_AMOUNT");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("ORDERS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ORDER_DETAIL_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailID"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("ORDER_ID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PRICE");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCT_ID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("QUANTITY");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TOTAL");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ORDER_DETAILS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PRODUCT_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CATEGORY_ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IMAGE_URL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NAME");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PRICE");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("PRODUCTS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ROLE_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ROLE_NAME");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ROLES", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FIRSTNAME");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("LASTNAME");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasColumnName("PASSWORD_HASH");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Lastname" }, "IX_LASTNAME");

                    b.HasIndex(new[] { "Email" }, "UQ_EMAIL")
                        .IsUnique();

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("USER_ROLES", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Wishlist", b =>
                {
                    b.Property<int>("WishlistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WISHLIST_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WishlistId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.HasKey("WishlistId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("WISHLISTS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.WishlistItem", b =>
                {
                    b.Property<int>("WishlistItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WISHLIST_ITEM_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WishlistItemId"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ADDED_AT");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("PRODUCT_ID");

                    b.Property<int>("WishlistId")
                        .HasColumnType("int")
                        .HasColumnName("WISHLIST_ID");

                    b.HasKey("WishlistItemId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WishlistId");

                    b.ToTable("WISHLIST_ITEMS", (string)null);
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Basket", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.User", "User")
                        .WithOne("Basket")
                        .HasForeignKey("Project_Keystone.Core.Entities.Basket", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.BasketItem", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.Basket", "Basket")
                        .WithMany("BasketItems")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Keystone.Core.Entities.Product", "Product")
                        .WithMany("BasketItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Order", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.OrderDetail", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Keystone.Core.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Product", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.UserRole", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Keystone.Core.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Wishlist", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.User", "User")
                        .WithOne("Wishlist")
                        .HasForeignKey("Project_Keystone.Core.Entities.Wishlist", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.WishlistItem", b =>
                {
                    b.HasOne("Project_Keystone.Core.Entities.Product", "Product")
                        .WithMany("WishlistItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Keystone.Core.Entities.Wishlist", "Wishlist")
                        .WithMany("WishListItems")
                        .HasForeignKey("WishlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Wishlist");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Basket", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Product", b =>
                {
                    b.Navigation("BasketItems");

                    b.Navigation("OrderDetails");

                    b.Navigation("WishlistItems");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.User", b =>
                {
                    b.Navigation("Basket");

                    b.Navigation("Orders");

                    b.Navigation("UserRoles");

                    b.Navigation("Wishlist");
                });

            modelBuilder.Entity("Project_Keystone.Core.Entities.Wishlist", b =>
                {
                    b.Navigation("WishListItems");
                });
#pragma warning restore 612, 618
        }
    }
}