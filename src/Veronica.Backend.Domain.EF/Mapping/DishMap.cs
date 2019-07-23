using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veronica.Backend.Domain.Models;

namespace Veronica.Backend.Domain.EF.Mapping
{
    public class DishMap : EntityTypeConfiguration<Dish>
    {
        public override void Map(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dishes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode();

            builder.Property(x => x.Score)
                .HasColumnType("numeric(2,1)");

            builder.Property(x => x.Added)
                .IsRequired()
                .HasColumnType("datetimeoffset(7)");

            builder.Property(x => x.LastInMenu)
                .HasColumnType("datetimeoffset(7)");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Dishes)
                .HasForeignKey(x => x.UserId);
        }
    }
}