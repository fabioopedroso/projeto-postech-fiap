using Core.Entity;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var emailConverter = new ValueConverter<Email, string>(
                email => email.ToString(),
                value => new Email(value)
            );

        var passwordConverter = new ValueConverter<Password, string>(
                password => password.Hashed,
                value => new Password(value)
            );

        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(u => u.CreationDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(u => u.IsActive).IsRequired().HasColumnType("BOOLEAN");

        builder.Property(u => u.UserName).IsRequired().HasColumnType("VARCHAR(100)");
        builder.Property(u => u.Email).IsRequired().HasConversion(emailConverter).HasColumnType("VARCHAR(254)");
        builder.Property(u => u.Password).IsRequired().HasConversion(passwordConverter).HasColumnType("VARCHAR(255)");
        builder.Property(u => u.UserType).IsRequired().HasColumnType("INT");
    }
}
