using ChatBotPlan.Domain;
using ChatBotPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBotPlan.Infrastructure;

public class UserVerificationCodeConfiguration : IEntityTypeConfiguration<UserVerificationCode>
{
    public void Configure(EntityTypeBuilder<UserVerificationCode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(6);
        builder.Property(x => x.Type).HasConversion<string>();
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
    }
}