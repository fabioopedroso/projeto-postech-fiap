using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart");
        builder.HasKey(c => c.Id);
        builder.Property(s => s.Id).HasColumnType("INT").UseIdentityColumn();

        builder.HasOne(c => c.User)
               .WithOne(u => u.Cart)
               .HasForeignKey<Cart>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Games)
               .WithMany(g => g.Carts)
               .UsingEntity(j => j.ToTable("CartGame"));
    }
}
