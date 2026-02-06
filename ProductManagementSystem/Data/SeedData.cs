using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Data;

public static class SeedData
{
    public static async Task InitializeDataAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        string[] roleNames = { "Admin", "Manager", "Employee" };

        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
        );

        // Ensure database exists
        await context.Database.MigrateAsync();

        // Seed Roles
        foreach (var roleName in roleNames)
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));

        const string adminEmail = "admin@test.com";
        const string employeeEmail = "test@test.com";
        const string seedPassword = "Password1!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, seedPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
        }

        var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
        if (employeeUser is null)
        {
            employeeUser = new ApplicationUser
            {
                UserName = employeeEmail,
                Email = employeeEmail,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(employeeUser, seedPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to create employee user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            await userManager.AddToRoleAsync(adminUser, "Admin");

        if (!await userManager.IsInRoleAsync(employeeUser, "Employee"))
            await userManager.AddToRoleAsync(employeeUser, "Employee");

        // If the table already contains products data, skip seeding
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
            new()
            {
                ProductName = "4K Action Camera",
                Category = "Electronics",
                Price = 299.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 7, 15),
                IsActive = false,
            },
            new()
            {
                ProductName = "Ergonomic Office Chair",
                Category = "Furniture",
                Price = 249.00m,
                Quantity = 25,
                DateAdded = new DateTime(2025, 1, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Cotton Bath Towel Set - 6pc",
                Category = "Home",
                Price = 39.99m,
                Quantity = 180,
                DateAdded = new DateTime(2024, 12, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Protein Powder - Chocolate 2lb",
                Category = "Health",
                Price = 34.95m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 8, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Digital Alarm Clock",
                Category = "Home",
                Price = 19.99m,
                Quantity = 145,
                DateAdded = new DateTime(2025, 3, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Stainless Steel Cookware Set - 12pc",
                Category = "Kitchen",
                Price = 189.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 10, 22),
                IsActive = false,
            },
            new()
            {
                ProductName = "Women's Running Tank Top",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 4, 7),
                IsActive = true,
            },
            new()
            {
                ProductName = "Cordless Drill Set",
                Category = "Tools",
                Price = 119.99m,
                Quantity = 35,
                DateAdded = new DateTime(2024, 11, 30),
                IsActive = true,
            },
            new()
            {
                ProductName = "Gel Pen Set - 24 Colors",
                Category = "Office",
                Price = 15.50m,
                Quantity = 210,
                DateAdded = new DateTime(2025, 2, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Dog Chew Toys - 5 Pack",
                Category = "Pet Supplies",
                Price = 16.99m,
                Quantity = 155,
                DateAdded = new DateTime(2025, 1, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Decorative Throw Pillows - Set of 4",
                Category = "Home",
                Price = 44.99m,
                Quantity = 72,
                DateAdded = new DateTime(2024, 9, 9),
                IsActive = true,
            },
            new()
            {
                ProductName = "Basketball - Official Size",
                Category = "Sports",
                Price = 29.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 7, 25),
                IsActive = false,
            },
            new()
            {
                ProductName = "Wall-Mounted Coat Rack",
                Category = "Furniture",
                Price = 32.50m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 3, 3),
                IsActive = true,
            },
            new()
            {
                ProductName = "Organic Honey - 16oz",
                Category = "Grocery",
                Price = 12.99m,
                Quantity = 240,
                DateAdded = new DateTime(2024, 12, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Leather Belt",
                Category = "Accessories",
                Price = 29.99m,
                Quantity = 115,
                DateAdded = new DateTime(2025, 2, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Tablet Stand - Adjustable",
                Category = "Electronics",
                Price = 24.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 8, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Toddler Puzzle Set - Animals",
                Category = "Toys",
                Price = 18.99m,
                Quantity = 130,
                DateAdded = new DateTime(2025, 1, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Facial Moisturizer - SPF 30",
                Category = "Beauty",
                Price = 22.50m,
                Quantity = 165,
                DateAdded = new DateTime(2024, 11, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Car Phone Holder - Magnetic",
                Category = "Automotive",
                Price = 12.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 10, 15),
                IsActive = false,
            },
            new()
            {
                ProductName = "Microfiber Cleaning Cloths - 12 Pack",
                Category = "Home",
                Price = 11.99m,
                Quantity = 320,
                DateAdded = new DateTime(2025, 5, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Gaming Mouse Pad - XL",
                Category = "Electronics",
                Price = 19.99m,
                Quantity = 98,
                DateAdded = new DateTime(2025, 4, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Hiking Backpack - 40L",
                Category = "Outdoors",
                Price = 79.99m,
                Quantity = 22,
                DateAdded = new DateTime(2024, 9, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Essential Oil Diffuser",
                Category = "Home",
                Price = 29.99m,
                Quantity = 142,
                DateAdded = new DateTime(2025, 3, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Wool Socks - 6 Pair",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 10),
                IsActive = false,
            },
            new()
            {
                ProductName = "Electric Toothbrush",
                Category = "Health",
                Price = 49.99m,
                Quantity = 68,
                DateAdded = new DateTime(2025, 2, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Non-Slip Rug Pad - 5x7",
                Category = "Home",
                Price = 19.99m,
                Quantity = 175,
                DateAdded = new DateTime(2024, 12, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "USB Flash Drive - 64GB",
                Category = "Electronics",
                Price = 14.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 8, 30),
                IsActive = true,
            },
            new()
            {
                ProductName = "Mixing Bowl Set - Stainless Steel",
                Category = "Kitchen",
                Price = 27.99m,
                Quantity = 105,
                DateAdded = new DateTime(2025, 4, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Insulated Travel Mug - 20oz",
                Category = "Kitchen",
                Price = 22.99m,
                Quantity = 189,
                DateAdded = new DateTime(2025, 1, 9),
                IsActive = true,
            },
            new()
            {
                ProductName = "Baby Monitor with Camera",
                Category = "Baby",
                Price = 89.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 6, 20),
                IsActive = false,
            },
            new()
            {
                ProductName = "Cordless Vacuum Cleaner",
                Category = "Home",
                Price = 179.99m,
                Quantity = 18,
                DateAdded = new DateTime(2024, 11, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Sunglasses - Polarized",
                Category = "Accessories",
                Price = 39.99m,
                Quantity = 92,
                DateAdded = new DateTime(2025, 5, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Dumbbell Set - 20lb Pair",
                Category = "Sports",
                Price = 54.99m,
                Quantity = 45,
                DateAdded = new DateTime(2025, 3, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Picture Frame Set - 8x10",
                Category = "Home",
                Price = 29.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 7, 30),
                IsActive = true,
            },
            new()
            {
                ProductName = "Smart LED Light Bulbs - 4 Pack",
                Category = "Electronics",
                Price = 44.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 2, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Grilling Tools Set - 3pc",
                Category = "Outdoors",
                Price = 24.99m,
                Quantity = 110,
                DateAdded = new DateTime(2024, 10, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Women's Crossbody Bag",
                Category = "Accessories",
                Price = 49.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 8, 12),
                IsActive = false,
            },
            new()
            {
                ProductName = "Desk Calendar 2025",
                Category = "Office",
                Price = 9.99m,
                Quantity = 200,
                DateAdded = new DateTime(2024, 12, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Inflatable Pool Float",
                Category = "Toys",
                Price = 19.99m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 4, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Ceramic Plant Pot - Large",
                Category = "Garden",
                Price = 24.99m,
                Quantity = 135,
                DateAdded = new DateTime(2025, 1, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wireless Earbuds",
                Category = "Electronics",
                Price = 69.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 9, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Canvas Tote Bag",
                Category = "Accessories",
                Price = 14.99m,
                Quantity = 240,
                DateAdded = new DateTime(2025, 3, 17),
                IsActive = true,
            },
            new()
            {
                ProductName = "Laundry Hamper - Collapsible",
                Category = "Home",
                Price = 19.99m,
                Quantity = 95,
                DateAdded = new DateTime(2024, 11, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Protein Bars - Variety Pack 12ct",
                Category = "Grocery",
                Price = 19.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 6, 8),
                IsActive = false,
            },
            new()
            {
                ProductName = "Slow Cooker - 6 Quart",
                Category = "Kitchen",
                Price = 49.99m,
                Quantity = 32,
                DateAdded = new DateTime(2025, 2, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Exercise Resistance Bands Set",
                Category = "Sports",
                Price = 17.99m,
                Quantity = 170,
                DateAdded = new DateTime(2025, 4, 30),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Dress Shirt - White",
                Category = "Clothing",
                Price = 34.99m,
                Quantity = 62,
                DateAdded = new DateTime(2024, 12, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "HDMI Cable - 6ft",
                Category = "Electronics",
                Price = 9.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wooden Cutting Board - Large",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 118,
                DateAdded = new DateTime(2025, 5, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Baby Blanket - Organic Cotton",
                Category = "Baby",
                Price = 24.99m,
                Quantity = 145,
                DateAdded = new DateTime(2025, 1, 30),
                IsActive = true,
            },
            new()
            {
                ProductName = "Computer Monitor Stand",
                Category = "Office",
                Price = 34.99m,
                Quantity = 2,
                DateAdded = new DateTime(2024, 8, 22),
                IsActive = false,
            },
            new()
            {
                ProductName = "Outdoor String Lights - 48ft",
                Category = "Garden",
                Price = 29.99m,
                Quantity = 78,
                DateAdded = new DateTime(2025, 3, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Shampoo - Natural Formula 16oz",
                Category = "Beauty",
                Price = 12.99m,
                Quantity = 210,
                DateAdded = new DateTime(2024, 10, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Cat Scratching Post",
                Category = "Pet Supplies",
                Price = 29.99m,
                Quantity = 54,
                DateAdded = new DateTime(2025, 2, 14),
                IsActive = true,
            },
            new()
            {
                ProductName = "Thermal Coffee Carafe - 1L",
                Category = "Kitchen",
                Price = 34.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 9, 14),
                IsActive = true,
            },
            new()
            {
                ProductName = "Air Purifier - Small Room",
                Category = "Home",
                Price = 79.99m,
                Quantity = 28,
                DateAdded = new DateTime(2024, 11, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Children's Art Supply Kit",
                Category = "Toys",
                Price = 29.99m,
                Quantity = 165,
                DateAdded = new DateTime(2025, 4, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Windshield Sun Shade",
                Category = "Automotive",
                Price = 14.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 6, 15),
                IsActive = false,
            },
            new()
            {
                ProductName = "Cooling Gel Pillow",
                Category = "Home",
                Price = 39.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 1, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wireless Keyboard and Mouse Combo",
                Category = "Electronics",
                Price = 44.99m,
                Quantity = 112,
                DateAdded = new DateTime(2025, 3, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Stainless Steel Food Storage Set",
                Category = "Kitchen",
                Price = 32.99m,
                Quantity = 98,
                DateAdded = new DateTime(2024, 12, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Women's Yoga Pants",
                Category = "Clothing",
                Price = 39.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 7, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Magnetic Whiteboard - 24x36",
                Category = "Office",
                Price = 34.99m,
                Quantity = 45,
                DateAdded = new DateTime(2025, 2, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bike Repair Tool Kit",
                Category = "Sports",
                Price = 24.99m,
                Quantity = 128,
                DateAdded = new DateTime(2025, 5, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Digital Kitchen Scale",
                Category = "Kitchen",
                Price = 19.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 8, 8),
                IsActive = false,
            },
            new()
            {
                ProductName = "Decorative Wall Clock",
                Category = "Home",
                Price = 29.99m,
                Quantity = 102,
                DateAdded = new DateTime(2025, 4, 5),
                IsActive = true,
            },
            new()
            {
                ProductName = "Travel Backpack - 25L",
                Category = "Accessories",
                Price = 54.99m,
                Quantity = 58,
                DateAdded = new DateTime(2024, 11, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Moisturizing Hand Cream - 3oz",
                Category = "Beauty",
                Price = 8.99m,
                Quantity = 280,
                DateAdded = new DateTime(2025, 1, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Portable Bluetooth Speaker - Mini",
                Category = "Electronics",
                Price = 24.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 9, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Stainless Steel Straws - 8 Pack",
                Category = "Kitchen",
                Price = 12.99m,
                Quantity = 195,
                DateAdded = new DateTime(2025, 3, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Athletic Shorts",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 115,
                DateAdded = new DateTime(2024, 12, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Surge Protector Power Strip - 6 Outlet",
                Category = "Electronics",
                Price = 19.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 6, 25),
                IsActive = false,
            },
            new()
            {
                ProductName = "Drawer Organizers - Set of 6",
                Category = "Home",
                Price = 17.99m,
                Quantity = 160,
                DateAdded = new DateTime(2025, 2, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bamboo Serving Tray",
                Category = "Kitchen",
                Price = 22.99m,
                Quantity = 92,
                DateAdded = new DateTime(2025, 5, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Football - Youth Size",
                Category = "Sports",
                Price = 19.99m,
                Quantity = 72,
                DateAdded = new DateTime(2024, 10, 3),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wooden Toy Train Set",
                Category = "Toys",
                Price = 34.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 8, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Glass Water Pitcher - 64oz",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 138,
                DateAdded = new DateTime(2025, 1, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Umbrella - Compact Travel",
                Category = "Accessories",
                Price = 16.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 22),
                IsActive = false,
            },
            new()
            {
                ProductName = "Makeup Brush Set - 12pc",
                Category = "Beauty",
                Price = 29.99m,
                Quantity = 105,
                DateAdded = new DateTime(2025, 4, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Acrylic Photo Frame - 5x7",
                Category = "Home",
                Price = 14.99m,
                Quantity = 175,
                DateAdded = new DateTime(2024, 11, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Car Tire Pressure Gauge",
                Category = "Automotive",
                Price = 9.99m,
                Quantity = 210,
                DateAdded = new DateTime(2025, 3, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Multivitamin Gummies - 60ct",
                Category = "Health",
                Price = 14.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 9, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Quilted Table Runner",
                Category = "Home",
                Price = 19.99m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 2, 6),
                IsActive = true,
            },
            new()
            {
                ProductName = "Webcam - 1080p HD",
                Category = "Electronics",
                Price = 49.99m,
                Quantity = 42,
                DateAdded = new DateTime(2024, 12, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Ice Cube Trays - Silicone 2pk",
                Category = "Kitchen",
                Price = 11.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 6, 30),
                IsActive = false,
            },
            new()
            {
                ProductName = "Women's Winter Gloves",
                Category = "Accessories",
                Price = 17.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 1, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Jigsaw Puzzle - 1000 pieces",
                Category = "Games",
                Price = 19.99m,
                Quantity = 98,
                DateAdded = new DateTime(2025, 5, 1),
                IsActive = true,
            },
            new()
            {
                ProductName = "Extension Cord - 25ft Heavy Duty",
                Category = "Tools",
                Price = 24.99m,
                Quantity = 72,
                DateAdded = new DateTime(2024, 10, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Aquarium Starter Kit - 5 Gallon",
                Category = "Pet Supplies",
                Price = 59.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 7, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Dish Drying Rack - Stainless Steel",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 110,
                DateAdded = new DateTime(2025, 3, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Memory Foam Bath Mat",
                Category = "Home",
                Price = 19.99m,
                Quantity = 155,
                DateAdded = new DateTime(2024, 11, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Polo Shirt",
                Category = "Clothing",
                Price = 29.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 8, 16),
                IsActive = false,
            },
            new()
            {
                ProductName = "Bluetooth FM Transmitter",
                Category = "Automotive",
                Price = 19.99m,
                Quantity = 82,
                DateAdded = new DateTime(2025, 2, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Recipe Box - Wooden",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 4, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Camping Chair - Foldable",
                Category = "Outdoors",
                Price = 34.99m,
                Quantity = 55,
                DateAdded = new DateTime(2024, 9, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wall-Mounted Key Holder",
                Category = "Home",
                Price = 14.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 6, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Printer Paper - 500 Sheets",
                Category = "Office",
                Price = 12.99m,
                Quantity = 340,
                DateAdded = new DateTime(2025, 1, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Baby Teething Toys - 5 Pack",
                Category = "Baby",
                Price = 16.99m,
                Quantity = 198,
                DateAdded = new DateTime(2025, 5, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Compression Socks - 3 Pair",
                Category = "Health",
                Price = 19.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 7, 28),
                IsActive = false,
            },
            new()
            {
                ProductName = "WiFi Range Extender",
                Category = "Electronics",
                Price = 39.99m,
                Quantity = 38,
                DateAdded = new DateTime(2025, 3, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Silicone Oven Mitts - Pair",
                Category = "Kitchen",
                Price = 14.99m,
                Quantity = 165,
                DateAdded = new DateTime(2024, 12, 3),
                IsActive = true,
            },
            new()
            {
                ProductName = "Children's Backpack - Cartoon Design",
                Category = "Accessories",
                Price = 24.99m,
                Quantity = 92,
                DateAdded = new DateTime(2025, 2, 16),
                IsActive = true,
            },
            new()
            {
                ProductName = "Garden Pruning Shears",
                Category = "Garden",
                Price = 17.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 8, 4),
                IsActive = true,
            },
            new()
            {
                ProductName = "Dry Erase Markers - 12 Pack",
                Category = "Office",
                Price = 9.99m,
                Quantity = 275,
                DateAdded = new DateTime(2025, 4, 22),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bathtub Drain Stopper",
                Category = "Home",
                Price = 8.99m,
                Quantity = 180,
                DateAdded = new DateTime(2024, 10, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Smart Watch Band - Silicone",
                Category = "Electronics",
                Price = 12.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 6, 5),
                IsActive = false,
            },
            new()
            {
                ProductName = "Measuring Cups and Spoons Set",
                Category = "Kitchen",
                Price = 14.99m,
                Quantity = 220,
                DateAdded = new DateTime(2025, 1, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Basketball Hoop - Door Mount",
                Category = "Sports",
                Price = 19.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 5, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Women's Tank Top - Pack of 3",
                Category = "Clothing",
                Price = 24.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 9, 15),
                IsActive = true,
            },
            new()
            {
                ProductName = "Shoe Rack - 4 Tier",
                Category = "Home",
                Price = 29.99m,
                Quantity = 62,
                DateAdded = new DateTime(2024, 11, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Hand Sanitizer - 8oz Pump",
                Category = "Health",
                Price = 6.99m,
                Quantity = 450,
                DateAdded = new DateTime(2025, 3, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "LED Strip Lights - 16ft",
                Category = "Electronics",
                Price = 24.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 7, 8),
                IsActive = false,
            },
            new()
            {
                ProductName = "Pizza Stone - 14 inch",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 98,
                DateAdded = new DateTime(2025, 2, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Pet Hair Roller",
                Category = "Pet Supplies",
                Price = 11.99m,
                Quantity = 215,
                DateAdded = new DateTime(2024, 12, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Closet Organizer Hanging Shelves",
                Category = "Home",
                Price = 19.99m,
                Quantity = 105,
                DateAdded = new DateTime(2025, 4, 16),
                IsActive = true,
            },
            new()
            {
                ProductName = "Document Shredder - Personal",
                Category = "Office",
                Price = 49.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 8, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Car Floor Mats - Universal",
                Category = "Automotive",
                Price = 29.99m,
                Quantity = 68,
                DateAdded = new DateTime(2025, 1, 16),
                IsActive = true,
            },
            new()
            {
                ProductName = "Sewing Kit - 100 Pieces",
                Category = "Crafts",
                Price = 14.99m,
                Quantity = 145,
                DateAdded = new DateTime(2025, 5, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Nightlight - Motion Sensor",
                Category = "Home",
                Price = 12.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 9, 20),
                IsActive = false,
            },
            new()
            {
                ProductName = "Coloring Book - Adult Mandalas",
                Category = "Books",
                Price = 8.99m,
                Quantity = 190,
                DateAdded = new DateTime(2025, 3, 6),
                IsActive = true,
            },
            new()
            {
                ProductName = "Nail Clipper Set - 5 Piece",
                Category = "Beauty",
                Price = 11.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 6, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Spice Rack - Rotating",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 78,
                DateAdded = new DateTime(2025, 2, 23),
                IsActive = true,
            },
            new()
            {
                ProductName = "Meditation Cushion",
                Category = "Health",
                Price = 34.99m,
                Quantity = 52,
                DateAdded = new DateTime(2024, 10, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Jump Rope - Adjustable",
                Category = "Sports",
                Price = 12.99m,
                Quantity = 185,
                DateAdded = new DateTime(2025, 4, 6),
                IsActive = true,
            },
            new()
            {
                ProductName = "Shower Curtain - Waterproof",
                Category = "Home",
                Price = 16.99m,
                Quantity = 112,
                DateAdded = new DateTime(2024, 11, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Men's Baseball Cap",
                Category = "Accessories",
                Price = 19.99m,
                Quantity = 4,
                DateAdded = new DateTime(2024, 7, 16),
                IsActive = false,
            },
            new()
            {
                ProductName = "USB-C Hub - 7 in 1",
                Category = "Electronics",
                Price = 34.99m,
                Quantity = 48,
                DateAdded = new DateTime(2025, 1, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Cocktail Shaker Set",
                Category = "Kitchen",
                Price = 24.99m,
                Quantity = 95,
                DateAdded = new DateTime(2025, 5, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Dog Leash - Retractable",
                Category = "Pet Supplies",
                Price = 19.99m,
                Quantity = 138,
                DateAdded = new DateTime(2024, 12, 16),
                IsActive = true,
            },
            new()
            {
                ProductName = "Throw Blanket - Fleece",
                Category = "Home",
                Price = 22.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 8, 10),
                IsActive = true,
            },
            new()
            {
                ProductName = "Sticky Notes - Assorted Colors",
                Category = "Office",
                Price = 7.99m,
                Quantity = 380,
                DateAdded = new DateTime(2025, 3, 12),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bike Water Bottle Holder",
                Category = "Sports",
                Price = 9.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 6, 22),
                IsActive = false,
            },
            new()
            {
                ProductName = "Lint Remover - Electric",
                Category = "Home",
                Price = 14.99m,
                Quantity = 125,
                DateAdded = new DateTime(2025, 2, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Reading Glasses - +2.00",
                Category = "Health",
                Price = 12.99m,
                Quantity = 165,
                DateAdded = new DateTime(2024, 10, 20),
                IsActive = true,
            },
            new()
            {
                ProductName = "Guitar Strings - Steel",
                Category = "Music",
                Price = 9.99m,
                Quantity = 5,
                DateAdded = new DateTime(2024, 7, 2),
                IsActive = true,
            },
            new()
            {
                ProductName = "Baking Sheet Set - 3 Piece",
                Category = "Kitchen",
                Price = 29.99m,
                Quantity = 88,
                DateAdded = new DateTime(2025, 4, 24),
                IsActive = true,
            },
            new()
            {
                ProductName = "Baby Wipes - 480 Count",
                Category = "Baby",
                Price = 16.99m,
                Quantity = 280,
                DateAdded = new DateTime(2025, 1, 6),
                IsActive = true,
            },
            new()
            {
                ProductName = "Desktop Fan - USB Powered",
                Category = "Electronics",
                Price = 14.99m,
                Quantity = 9,
                DateAdded = new DateTime(2024, 9, 8),
                IsActive = false,
            },
            new()
            {
                ProductName = "Placemats - Set of 4",
                Category = "Kitchen",
                Price = 19.99m,
                Quantity = 115,
                DateAdded = new DateTime(2025, 5, 6),
                IsActive = true,
            },
            new()
            {
                ProductName = "Adjustable Wrench - 10 inch",
                Category = "Tools",
                Price = 12.99m,
                Quantity = 142,
                DateAdded = new DateTime(2024, 11, 2),
                IsActive = true,
            },
            new()
            {
                ProductName = "Women's Cardigan Sweater",
                Category = "Clothing",
                Price = 44.99m,
                Quantity = 6,
                DateAdded = new DateTime(2024, 6, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bath Bombs - 6 Pack",
                Category = "Beauty",
                Price = 18.99m,
                Quantity = 175,
                DateAdded = new DateTime(2025, 3, 28),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bookends - Metal Pair",
                Category = "Office",
                Price = 16.99m,
                Quantity = 92,
                DateAdded = new DateTime(2024, 12, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "Bike Lock - Cable",
                Category = "Sports",
                Price = 14.99m,
                Quantity = 7,
                DateAdded = new DateTime(2024, 8, 18),
                IsActive = false,
            },
            new()
            {
                ProductName = "Corkscrew - Wine Opener",
                Category = "Kitchen",
                Price = 11.99m,
                Quantity = 205,
                DateAdded = new DateTime(2025, 2, 4),
                IsActive = true,
            },
            new()
            {
                ProductName = "Wall Hooks - 10 Pack",
                Category = "Home",
                Price = 8.99m,
                Quantity = 310,
                DateAdded = new DateTime(2025, 5, 18),
                IsActive = true,
            },
            new()
            {
                ProductName = "Car Vacuum Cleaner - Handheld",
                Category = "Automotive",
                Price = 34.99m,
                Quantity = 3,
                DateAdded = new DateTime(2024, 9, 25),
                IsActive = true,
            },
            new()
            {
                ProductName = "First Aid Kit - 100 Piece",
                Category = "Health",
                Price = 24.99m,
                Quantity = 85,
                DateAdded = new DateTime(2025, 1, 14),
                IsActive = true,
            },
            new()
            {
                ProductName = "Phone Screen Protector - 3 Pack",
                Category = "Electronics",
                Price = 9.99m,
                Quantity = 8,
                DateAdded = new DateTime(2024, 6, 8),
                IsActive = true,
            },
            new()
            {
                ProductName = "Salad Spinner",
                Category = "Kitchen",
                Price = 19.99m,
                Quantity = 122,
                DateAdded = new DateTime(2025, 4, 2),
                IsActive = true,
            },
        };

        await context.AddRangeAsync(products);

        await context.SaveChangesAsync();
    }
}
