using Lib.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib.Repository.Repository;

public class BattleRepository : IBattleRepository
{
    private readonly BattleOfMonstersContext _context;

    public BattleRepository(BattleOfMonstersContext context)
    {
        this._context = context;
    }

    public ValueTask<EntityEntry<Battle>> AddAsync(Battle battle)
    {
        return this._context.Set<Battle>().AddAsync(battle);
    }

    public Task<Battle?> FindAsync(int id)
    {
        return this._context.Set<Battle>().FindAsync(id).AsTask();
    }

    public async Task<IEnumerable<Battle>> GetAllAsync()
    {
        return await this._context.Set<Battle>()
            .Include(x => x.MonsterARelation)
            .Include(x => x.MonsterBRelation)
            .Include(x => x.WinnerRelation)
            .ToArrayAsync();
    }

    public async Task<EntityEntry<Battle>?> RemoveAsync(int id)
    {
        Battle? entity = await this._context.Set<Battle>().FindAsync(id);
        return entity == null ? null : this._context.Set<Battle>().Remove(entity);
    }
}