using Lib.Repository.Entities;

namespace API.Test.Fixtures;

public static class BattlesFixture
{
    public static IEnumerable<Battle> GetBattlesMock()
    {
        return new[]
        {
            new Battle()
            {
                Id = 1,
                MonsterA = 1,
                MonsterB = 2,
                Winner = 1
            }
        };
    }
}