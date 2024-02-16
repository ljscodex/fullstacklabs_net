using Lib.Repository.Entities;
using Lib.Repository.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Lib.Repository;

public sealed class BattleOfMonstersContext : DbContext
{
    public DbSet<Battle> Battle { get; set; } = null!;
    public DbSet<Monster> Monster { get; set; } = null!;


    public BattleOfMonstersContext(DbContextOptions<BattleOfMonstersContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfiguration(new BattleMapping());
        modelBuilder.ApplyConfiguration(new MonsterMapping());

        modelBuilder.Entity<Monster>().HasMany<Battle>().WithOne(c => c.MonsterARelation).HasForeignKey(c => c.MonsterA).HasPrincipalKey(c => c.Id);
        modelBuilder.Entity<Monster>().HasMany<Battle>().WithOne(c => c.MonsterBRelation).HasForeignKey(c => c.MonsterB).HasPrincipalKey(c => c.Id);
        modelBuilder.Entity<Monster>().HasMany<Battle>().WithOne(c => c.WinnerRelation).HasForeignKey(c => c.Winner).HasPrincipalKey(c => c.Id);
    }
}