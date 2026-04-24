using ChatBotPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBotPlan.Infrastructure;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
        .HasColumnName("Name")
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(u => u.Email)
        .HasColumnName("Email")
        .HasMaxLength(100)
        .IsRequired();

        builder.HasIndex(u => u.Email)
        .IsUnique();

        builder.Property(u => u.PassWordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at");
        

    }
    
}