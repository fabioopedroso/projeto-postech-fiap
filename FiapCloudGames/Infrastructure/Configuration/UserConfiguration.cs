using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
        builder.Property(u => u.CreationDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(u => u.IsActive).IsRequired().HasColumnType("BIT");

        builder.Property(u => u.UserName).IsRequired().HasColumnType("VARCHAR(100)");
        builder.Property(u => u.Email).IsRequired().HasColumnType("VARCHAR(254)");
        builder.Property(u => u.Password).IsRequired().HasColumnType("VARCHAR(255)");
        builder.Property(u => u.UserType).IsRequired().HasColumnType("INT");
    }
}
