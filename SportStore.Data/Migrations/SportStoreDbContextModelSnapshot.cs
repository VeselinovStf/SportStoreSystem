﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportStore.Data;

namespace SportStore.Data.Migrations
{
    [DbContext(typeof(SportStoreDbContext))]
    partial class SportStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportStore.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("SportStore.Models.CardItem", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Card_Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("Card_Id");

                    b.ToTable("CardItems");
                });

            modelBuilder.Entity("SportStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SportStore.Models.CardItem", b =>
                {
                    b.HasOne("SportStore.Models.Card", "Card")
                        .WithMany("CardItems")
                        .HasForeignKey("Card_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportStore.Models.Product", "Product")
                        .WithOne("CardLine")
                        .HasForeignKey("SportStore.Models.CardItem", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
