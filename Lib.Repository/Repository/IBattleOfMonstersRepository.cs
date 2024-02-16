namespace Lib.Repository.Repository;

public interface IBattleOfMonstersRepository
{
    IMonsterRepository Monsters { get; }
    IBattleRepository Battles { get; }
    Task<int> Save();
}