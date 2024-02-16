using Lib.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lib.Repository.Mappings;

public class BattleMapping : IEntityTypeConfiguration<Battle>
{
    public void Configure(EntityTypeBuilder<Battle> builder)
    {
        builder.ToTable("Battle");

        builder.Property(p => p.Id).HasColumnType("INTEGER").IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.MonsterA).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.MonsterB).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.Winner).HasColumnType("INTEGER").IsRequired();

        builder.HasKey(p => p.Id);
    }
}