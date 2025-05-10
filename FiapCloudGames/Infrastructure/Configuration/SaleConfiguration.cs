using Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration;
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(s => s.CreationDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(s => s.IsActive).IsRequired().HasColumnType("BIT");

        builder.Property(s => s.Name).IsRequired().HasColumnType("VARCHAR(100)");
        builder.Property(s => s.Description).IsRequired().HasColumnType("VARCHAR(255)");
        builder.Property(s => s.StartDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(s => s.EndDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(s => s.DiscountPercentage).IsRequired().HasColumnType("NUMERIC(3,2)");

        builder
            .HasMany(s => s.Games)
            .WithMany(g => g.Sales)
            .UsingEntity(j => j.ToTable("GameSale"));
    }
}