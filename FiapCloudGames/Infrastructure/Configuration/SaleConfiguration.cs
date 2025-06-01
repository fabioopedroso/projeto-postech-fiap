using Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Core.ValueObjects;

namespace Infrastructure.Configuration;
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.OwnsOne(s => s.Period, period =>
        {
            period.Property(p => p.Start).HasColumnName("StartDate").HasColumnType("TIMESTAMPTZ").IsRequired();
            period.Property(p => p.End).HasColumnName("EndDate").HasColumnType("TIMESTAMPTZ").IsRequired();
        });

        var discountConverter = new ValueConverter<DiscountPercentage, decimal>(
            discount => discount.Value,
            value => new DiscountPercentage(value)
        );

        builder.ToTable("Sale");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(s => s.CreationDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(s => s.IsActive).IsRequired().HasColumnType("BOOLEAN");

        builder.Property(s => s.Name).IsRequired().HasColumnType("VARCHAR(100)");
        builder.Property(s => s.Description).IsRequired().HasColumnType("VARCHAR(255)");
        builder.Property(s => s.DiscountPercentage).IsRequired().HasConversion(discountConverter).HasColumnType("NUMERIC(3,2)");

        builder
            .HasMany(s => s.Games)
            .WithMany(g => g.Sales)
            .UsingEntity(j => j.ToTable("GameSale"));
    }
}