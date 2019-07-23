using Veronica.Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Veronica.Backend.Domain.EF.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode();

            builder.Property(x => x.RegistrationDate)
                .IsRequired()
                .HasColumnType("datetimeoffset(7)");

            builder.HasIndex(x => x.Email);
        }
    }
}
