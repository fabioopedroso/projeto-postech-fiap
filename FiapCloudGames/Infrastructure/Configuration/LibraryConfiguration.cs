using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class LibraryConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Library");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(l => l.UserId).IsRequired().HasColumnType("INT");
        builder.Property(l => l.GameId).IsRequired().HasColumnType("INT");
        builder.HasOne(l => l.User)
            .WithOne(u => u.Library)
            .HasForeignKey<Library>(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(l => l.Games)
            .WithMany(g => g.Libraries)
            .UsingEntity(j => j.ToTable("LibraryGame"));
    }
}
