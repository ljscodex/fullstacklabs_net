namespace Lib.Repository.Repository;

public class BattleOfMonstersRepository : IBattleOfMonstersRepository
{
    private readonly BattleOfMonstersContext _context;
    private BattleRepository? _battles;
    private MonsterRepository? _monsters;

    public BattleOfMonstersRepository(BattleOfMonstersContext context)
    {
        this._context = context;
    }

    public IBattleRepository Battles
    {
        get { return this._battles ??= new BattleRepository(this._context); }
    }

    public IMonsterRepository Monsters
    {
        get { return this._monsters ??= new MonsterRepository(this._context); }
    }

    public async Task<int> Save()
    {
        return await this._context.SaveChangesAsync();
    }
}