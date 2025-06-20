﻿using Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Core.ValueObjects;

namespace Infrastructure.Configuration;
public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        var priceConverter = new ValueConverter<Price, decimal>(
            price => price.Amount,
            value => new Price(value)
        );

        builder.ToTable("Game");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(g => g.CreationDate).IsRequired().HasColumnType("TIMESTAMP");
        builder.Property(g => g.IsActive).IsRequired().HasColumnType("BOOLEAN");

        builder.Property(g => g.Name).IsRequired().HasColumnType("VARCHAR(100)");
        builder.Property(g => g.Description).IsRequired().HasColumnType("VARCHAR(255)");
        builder.Property(g => g.Genre).IsRequired().HasColumnType("VARCHAR(100)");
        builder.Property(g => g.Price).IsRequired().HasConversion(priceConverter).HasColumnType("NUMERIC(18,2)");
    }
}