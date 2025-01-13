﻿// <auto-generated />
using Discount.GRPC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Discount.GRPC.Migrations
{
    [DbContext(typeof(DiscountDataContext))]
    partial class DiscountDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Discount.GRPC.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cuopons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 150,
                            Description = "IPhone discount",
                            ProductName = "IPhone X"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 300,
                            Description = "Samsung discount",
                            ProductName = "Samsung 10"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
