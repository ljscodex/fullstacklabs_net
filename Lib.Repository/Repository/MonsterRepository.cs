using Lib.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib.Repository.Repository;

public class MonsterRepository : IMonsterRepository
{
    private readonly BattleOfMonstersContext _context;

    public MonsterRepository(BattleOfMonstersContext context)
    {
        this._context = context;
    }
    
    public ValueTask<EntityEntry<Monster>> AddAsync(Monster monster)
    {
        return this._context.Set<Monster>().AddAsync(monster);
    }

    public Task AddAsync(IEnumerable<Monster> monsters)
    {
        return this._context.Set<Monster>().AddRangeAsync(monsters);
    }

    public ValueTask<Monster?> FindAsync(int? id)
    {
        return this._context.Set<Monster>().FindAsync(id);
    }

    public async Task<Monster[]> GetAllAsync()
    {
        return await this._context.Set<Monster>().ToArrayAsync();
    }

    public async Task<EntityEntry<Monster>?> RemoveAsync(int id)
    {
        Monster? entity = await this._context.Set<Monster>().FindAsync(id);
        return entity == null ? null : this._context.Set<Monster>().Remove(entity);
    }

    public void Update(int id, Monster monster)
    {
        Monster? entity = this._context.Set<Monster>().Find(id);

        if (entity != null)
        {
            this._context.Entry<Monster>(entity).CurrentValues.SetValues(monster);
        }
    }
}