using ComputerInventoryAPI.Models;

namespace ComputerInventoryAPI;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public DbSet<PC> Pc { get; set; }
    public DbSet<Component>  Components { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PcComponent>().HasKey(pc => new { pc.PcId, pc.ComponentCode });
        
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 2, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
        );
        
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "NV", FullName = "NVIDIA", FoundationDate = new DateTime(1993, 4, 5) },
            new ComponentManufacturer { Id = 2, Abbreviation = "INT", FullName = "Intel", FoundationDate = new DateTime(1968, 7, 18) },
            new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair", FoundationDate = new DateTime(1994, 1, 1) }
        );
        
        modelBuilder.Entity<Component>().HasData(
            new Component 
            { 
                Code = "RTX4080", 
                Name = "RTX 4080", 
                Description = "High-end Gaming GPU", 
                ComponentManufacturersId = 1, 
                ComponentTypesId = 1 
            },
            new Component 
            { 
                Code = "I9-14900K ", 
                Name = "Core i9", 
                Description = "Flagship Desktop Processor",
                ComponentManufacturersId = 2, 
                ComponentTypesId = 2 
            },
            new Component 
            { 
                Code = "VENGE-16GB", 
                Name = "Vengeance 16GB", 
                Description = "High-speed DDR5 RAM",
                ComponentManufacturersId = 3, 
                ComponentTypesId = 3 
            }
        );
        
        modelBuilder.Entity<PC>().HasData(
            new PC { Id = 1, Name = "Gaming Beast", Weight = 12.5f, Warranty = 36, CreatedAt = DateTime.Parse("2026-05-01"), Stock = 5 },
            new PC { Id = 2, Name = "Office Pro", Weight = 4.2f, Warranty = 24, CreatedAt = DateTime.Parse("2026-04-10"), Stock = 10 },
            new PC { Id = 3, Name = "Workstation", Weight = 15.0f, Warranty = 48, CreatedAt = DateTime.Parse("2026-05-10"), Stock = 2 }
        );
        
        modelBuilder.Entity<PcComponent>().HasData(
            new PcComponent { PcId = 1, ComponentCode = "RTX4080   ", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "VENGE-16GB", Amount = 2 },
            new PcComponent { PcId = 2, ComponentCode = "VENGE-16GB", Amount = 1 }
        );
    }
}