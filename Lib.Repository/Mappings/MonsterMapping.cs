using Lib.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lib.Repository.Mappings;

public class MonsterMapping : IEntityTypeConfiguration<Monster>
{
    public void Configure(EntityTypeBuilder<Monster> builder)
    {
        builder.ToTable("Monster");

        builder.Property(p => p.Id).HasColumnType("INTEGER").IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.Attack).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.Defense).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.Hp).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.Name).HasColumnType("TEXT").IsRequired();
        builder.Property(p => p.ImageUrl).HasColumnType("TEXT").IsRequired();
        builder.Property(p => p.Speed).HasColumnType("INTEGER").IsRequired();
        
        builder.HasKey(p => p.Id);
        
        builder.HasMany<Battle>().WithOne(c => c.MonsterARelation).HasForeignKey(c => c.MonsterA).HasPrincipalKey(c => c.Id);
        builder.HasMany<Battle>().WithOne(c => c.MonsterBRelation).HasForeignKey(c => c.MonsterB).HasPrincipalKey(c => c.Id);
        builder.HasMany<Battle>().WithOne(c => c.WinnerRelation).HasForeignKey(c => c.Winner).HasPrincipalKey(c => c.Id);

        builder.HasData(new Monster[]
        {
            new Monster
            {
                Id = 1,
                Name = "Dead Unicorn",
                Attack = 60,
                Defense = 40,
                Hp = 10,
                Speed = 80,
                ImageUrl = "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/dead-unicorn.png"
            },
            new Monster
            {
                Id = 2,
                Name = "Old Shark",
                Attack = 50,
                Defense = 20,
                Hp = 80,
                Speed = 90,
                ImageUrl = "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/old-shark.png"
            },
            new Monster
            {
                Id = 3,
                Name = "Red Dragon",
                Attack = 90,
                Defense = 80,
                Hp = 90,
                Speed = 70,
                ImageUrl = "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/red-dragon.png"
            },
            new Monster
            {
                Id = 4,
                Name = "Robot Bear",
                Attack = 50,
                Defense = 40,
                Hp = 80,
                Speed = 60,
                ImageUrl = "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/robot-bear.png"
            },
            new Monster
            {
                Id = 5,
                Name = "Angry Snake",
                Attack = 80,
                Defense = 20,
                Hp = 70,
                Speed = 80,
                ImageUrl = "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/angry-snake.png"
            }
        });
    }
}