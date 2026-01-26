using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Data;

public static class SeedData
{
    public static async Task InitializeDataAsync(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
        );

        // Ensure database exists
        await context.Database.EnsureCreatedAsync();

        // If table already contains data, skip seeding
        if (await context.Products.AnyAsync())
            return;

        var products = new List<Product>
        {
            new()
            {
                ProductName = "Wireless Mouse",
                Category = "Electronics",
                Price = 29.99m,
                Quantity = 150,
                DateAdded = new DateTime(2025, 6, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Mechanical Keyboard",
                Category = "Electronics",
                Price = 89.50m,
                Quantity = 75,
                DateAdded = new DateTime(2025, 5, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Stainless Steel Water Bottle",
                Category = "Home",
                Price = 19.95m,
                Quantity = 200,
                DateAdded = new DateTime(2025, 2, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Organic Green Tea (100 bags)",
                Category = "Grocery",
                Price = 12.00m,
                Quantity = 500,
                DateAdded = new DateTime(2024, 11, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Denim Jacket",
                Category = "Clothing",
                Price = 59.99m,
                Quantity = 40,
                DateAdded = new DateTime(2025, 3, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Children's Building Blocks Set",
                Category = "Toys",
                Price = 24.99m,
                Quantity = 120,
                DateAdded = new DateTime(2025, 4, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Yoga Mat - Non Slip",
                Category = "Sports",
                Price = 34.00m,
                Quantity = 80,
                DateAdded = new DateTime(2025, 1, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Noise-Cancelling Headphones",
                Category = "Electronics",
                Price = 199.99m,
                Quantity = 25,
                DateAdded = new DateTime(2024, 12, 1),
                IsActive = false,
            },
            new()
            {
                ProductName = "Hardcover Notebook - 200 pages",
                Category = "Books",
                Price = 8.50m,
                Quantity = 300,
                DateAdded = new DateTime(2025, 6, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Vitamin D 1000IU - 120 capsules",
                Category = "Health",
                Price = 14.75m,
                Quantity = 60,
                DateAdded = new DateTime(2024, 9, 30),
                IsActive = true,
            },
            // 30 additional items
            new()
            {
                ProductName = "Smartphone Car Mount",
                Category = "Automotive",
                Price = 15.99m,
                Quantity = 220,
                DateAdded = new DateTime(2025, 2, 2),
                IsActive = true,
            },
            new()
            {
                ProductName = "LED Desk Lamp with USB",
                Category = "Home",
                Price = 27.49m,
                Quantity = 140,
                DateAdded = new DateTime(2025, 3, 14),
                IsActive = true,
            },
            new()
            {
                ProductName = "Ceramic Coffee Mug - 12oz",
                Category = "Kitchen",
                Price = 9.99m,
                Quantity = 360,
                DateAdded = new DateTime(2024, 10, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wireless Charger Pad",
                Category = "Electronics",
                Price = 22.00m,
                Quantity = 180,
                DateAdded = new DateTime(2025, 4, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Eco-friendly Bamboo Toothbrush (4-pack)",
                Category = "Health",
                Price = 7.50m,
                Quantity = 420,
                DateAdded = new DateTime(2024, 8, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Adjustable Laptop Stand",
                Category = "Office",
                Price = 39.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 1, 7),
                IsActive = true,
            },
            new()
            {
                ProductName = "Stain-Resistant Sofa Cover - Large",
                Category = "Home",
                Price = 64.99m,
                Quantity = 50,
                DateAdded = new DateTime(2025, 5, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Kids' Puzzle 500 pcs",
                Category = "Toys",
                Price = 14.99m,
                Quantity = 210,
                DateAdded = new DateTime(2025, 2, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Trail Running Shoes - Men's 10",
                Category = "Shoes",
                Price = 89.99m,
                Quantity = 30,
                DateAdded = new DateTime(2025, 3, 30),
                IsActive = true,
            },
            new()
            {
                ProductName = "Stainless Cutlery Set - 16pc",
                Category = "Kitchen",
                Price = 34.99m,
                Quantity = 110,
                DateAdded = new DateTime(2024, 12, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bluetooth Speaker - Waterproof",
                Category = "Electronics",
                Price = 49.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 1, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Portable Power Bank 20000mAh",
                Category = "Electronics",
                Price = 39.50m,
                Quantity = 130,
                DateAdded = new DateTime(2024, 11, 2),
                IsActive = true,
            },
            new()
            {
                ProductName = "Non-stick Frying Pan 10 inch",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 70,
                DateAdded = new DateTime(2025, 4, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Desk Organizer - Multi Compartment",
                Category = "Office",
                Price = 12.99m,
                Quantity = 250,
                DateAdded = new DateTime(2025, 5, 2),
                IsActive = true,
            },
            new()
            {
                ProductName = "Garden Hose 50ft",
                Category = "Garden",
                Price = 29.95m,
                Quantity = 60,
                DateAdded = new DateTime(2024, 9, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Pet Bed - Medium",
                Category = "Pet Supplies",
                Price = 22.00m,
                Quantity = 90,
                DateAdded = new DateTime(2025, 2, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Children's Raincoat - Age 4-6",
                Category = "Clothing",
                Price = 19.99m,
                Quantity = 75,
                DateAdded = new DateTime(2025, 3, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Electric Kettle 1.7L",
                Category = "Kitchen",
                Price = 34.99m,
                Quantity = 48,
                DateAdded = new DateTime(2024, 12, 6),
                IsActive = true,
            },
            new()
            {
                ProductName = "Metal Screwdriver Set - 10pc",
                Category = "Tools",
                Price = 17.50m,
                Quantity = 160,
                DateAdded = new DateTime(2025, 1, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Classic Vinyl Record - Rock Hits",
                Category = "Music",
                Price = 21.00m,
                Quantity = 40,
                DateAdded = new DateTime(2024, 10, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Streaming Media Player",
                Category = "Electronics",
                Price = 59.99m,
                Quantity = 55,
                DateAdded = new DateTime(2025, 6, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Silicone Baking Mat - Set of 2",
                Category = "Kitchen",
                Price = 13.99m,
                Quantity = 140,
                DateAdded = new DateTime(2025, 4, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Women's Lightweight Scarf",
                Category = "Accessories",
                Price = 11.50m,
                Quantity = 220,
                DateAdded = new DateTime(2025, 2, 27),
                IsActive = true,
            },
            new()
            {
                ProductName = "Board Game - Strategy Edition",
                Category = "Games",
                Price = 44.99m,
                Quantity = 65,
                DateAdded = new DateTime(2025, 3, 9),
                IsActive = true,
            },
            new()
            {
                ProductName = "Reusable Grocery Tote - 3 pack",
                Category = "Home",
                Price = 9.50m,
                Quantity = 300,
                DateAdded = new DateTime(2024, 11, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Camping Lantern - LED",
                Category = "Outdoors",
                Price = 18.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 5, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Leather Card Wallet",
                Category = "Accessories",
                Price = 24.00m,
                Quantity = 120,
                DateAdded = new DateTime(2025, 1, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Anti-Fog Swim Goggles",
                Category = "Sports",
                Price = 12.99m,
                Quantity = 180,
                DateAdded = new DateTime(2024, 9, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wireless Presentation Remote",
                Category = "Office",
                Price = 16.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 2, 9),
                IsActive = true,
            },
            new()
            {
                ProductName = "Aromatic Soy Candle - Lavender",
                Category = "Home",
                Price = 11.99m,
                Quantity = 200,
                DateAdded = new DateTime(2025, 4, 3),
                IsActive = true,
            },
        };

        await context.AddRangeAsync(products);

        await context.SaveChangesAsync();
    }
}
