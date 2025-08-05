using Microsoft.EntityFrameworkCore;
using Fridges.Domain.Enities.Products;
using Fridges.Domain.Enities.Recipe;
using Fridges.Domain.Enities;
using Fridges.Domain.Enums;
using System.Reflection.Emit;

namespace Fridges.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MilkProducts> MilkProducts { get; set; }
    public DbSet<MeatProducts> MeatProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PlantProducts> PlantProducts { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeProduct> RecipeProducts { get; set; }
    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
           .HasOne(p => p.Fridge)
           .WithMany(f => f.Products)
           .HasForeignKey(p => p.FridgeId);

        builder.Entity<MeatProducts>()
            .HasKey(mp => mp.ProductId);

        builder.Entity<MeatProducts>()
            .HasOne(mp => mp.Product)
            .WithOne()
            .HasForeignKey<MeatProducts>(mp => mp.ProductId);

        builder.Entity<PlantProducts>()
          .Property(mp => mp.Type)
          .HasConversion<string>();

        builder.Entity<MilkProducts>()
            .HasKey(mp => mp.ProductId);
        builder.Entity<MilkProducts>()
            .HasOne(mp => mp.Product)
            .WithOne()
            .HasForeignKey<MilkProducts>(mp => mp.ProductId);
        builder.Entity<MilkProducts>()
          .Property(mp => mp.Type)
          .HasConversion<string>();

        builder.Entity<PlantProducts>()
           .HasKey(pp => pp.ProductId);
        builder.Entity<PlantProducts>()
            .HasOne(pp => pp.Product)
            .WithOne()
            .HasForeignKey<PlantProducts>(p => p.ProductId);
        builder.Entity<PlantProducts>()
            .Property(pp => pp.Type)
            .HasConversion<string>();

        builder.Entity<Product>()
            .HasOne(pt => pt.ProductType)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.ProductTypeId);

        builder.Entity<RecipeProduct>()
            .HasKey(rp => rp.Id);

        builder.Entity<RecipeProduct>()
            .HasOne(rp => rp.Recipe)
            .WithMany(r => r.RecipeProducts)
            .HasForeignKey(rp => rp.RecipeId);

        builder.Entity<RecipeProduct>()
            .HasOne(rp => rp.ProductType)
            .WithMany(r => r.RecipeProducts)
            .HasForeignKey(rp => rp.ProductTypeId);

        builder.Entity<RecipeProduct>()
              .Property(pp => pp.unitType)
              .HasConversion<string>();

        builder.Entity<ProductType>()
            .Property(pt => pt.category)
            .HasConversion<string>();

        // Уникальные и валидные GUIDы
        Guid FridgeId_1 = Guid.Parse("e5820c2b-fc43-4a62-a717-b0eecb90e0b5");
        Guid FridgeId_2 = Guid.Parse("1aa76163-5ad2-4b36-a9cf-b04dd154a73f");

        builder.Entity<Fridge>().HasData(
            new Fridge { Id = FridgeId_1, Name = "Атлант", IsFreezer = false, Capacity = 5 },
            new Fridge { Id = FridgeId_2, Name = "Samsung", IsFreezer = true, Capacity = 7 }
        );

        var product1Id = Guid.Parse("0fa2a1f6-fc70-45e5-82e4-e25aa6f49b95");
        var product2Id = Guid.Parse("96a7c8a5-9517-4a39-a990-0145b17b7f4c");
        var product3Id = Guid.Parse("a7e58b11-d61a-4df3-9cb3-2d1272a99115");

        builder.Entity<Product>().HasData(
            new Product { Id = product1Id, Release = new DateOnly(2025, 7, 1), Expiration = new DateOnly(2025, 7, 20), Weight = 0.5f, IsFresh = true, FridgeId = FridgeId_2, ProductTypeId=3 },
            new Product { Id = product2Id, Release = new DateOnly(2025, 7, 2), Expiration = new DateOnly(2025, 7, 18), Weight = 1.2f, IsFresh = true, FridgeId = FridgeId_1,ProductTypeId=4 },
            new Product { Id = product3Id, Release = new DateOnly(2025, 7, 5), Expiration = new DateOnly(2025, 8, 1), Weight = 0.3f, IsFresh = true, FridgeId = FridgeId_2,ProductTypeId=11 }
        );

        builder.Entity<MeatProducts>().HasData(
            new MeatProducts { ProductId = product1Id, IsFrozen = true, Type = MeatProductsType.Chicken }
        );

        builder.Entity<MilkProducts>().HasData(
            new MilkProducts { ProductId = product2Id, FatPercent = 3.5f, Type = MilkProductsType.Milk }
        );

        builder.Entity<PlantProducts>().HasData(
            new PlantProducts { ProductId = product3Id, IsOrganic = true, Type = PlantProductsType.Tomato }
        );

        var Recipe_Id1 = Guid.Parse("22b1b28a-7712-4f3a-a4b2-0b5d79c6c230");
        builder.Entity<Recipe>().HasData(
            new Recipe { Id = Recipe_Id1, title = "Simple chicken pasta", Instructions = "Some instructions..." }
        );

        builder.Entity<ProductType>().HasData(
            new ProductType { Id = 1, Name = "Говядина" },
            new ProductType { Id = 2, Name = "Свинина" },
            new ProductType { Id = 3, Name = "Курица" },
            new ProductType { Id = 4, Name = "Молоко" },
            new ProductType { Id = 5, Name = "Сыр" },
            new ProductType { Id = 6, Name = "Йогурт" },
            new ProductType { Id = 7, Name = "Брокколи" },
            new ProductType { Id = 8, Name = "Картофель" },
            new ProductType { Id = 9, Name = "Яблоко" },
            new ProductType { Id = 10, Name = "Хлеб" },
            new ProductType { Id = 11, Name = "Томаты" }
        );

        Guid rpId1 = Guid.Parse("80ac650f-2158-4d5d-a38f-16e1287b89d4");
        Guid rpId2 = Guid.Parse("96e30ad8-94d9-4dc0-9f79-8a13adfe6de9");
        Guid rpId3 = Guid.Parse("b5384463-0404-42dc-b949-f0b33916efaa");

        builder.Entity<RecipeProduct>().HasData(
            new RecipeProduct { Id = rpId1, RecipeId = Recipe_Id1, ProductTypeId = 3, Quantity = 0.4f, unitType = UnitType.kg },
            new RecipeProduct { Id = rpId2, RecipeId = Recipe_Id1, ProductTypeId = 4, Quantity = 500, unitType = UnitType.ml },
            new RecipeProduct { Id = rpId3, RecipeId = Recipe_Id1, ProductTypeId = 11, Quantity = 200, unitType = UnitType.g }
        );
    }
}
