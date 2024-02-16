using Lib.Repository.Entities;

namespace API.Test.Fixtures;

public static class MonsterFixture
{
    public static IEnumerable<Monster> GetMonstersMock()
    {
        return new[]
        {
            new Monster
            {
                Id = 1,
                Name = "monster-1",
                Attack = 40,
                Defense = 20,
                Hp = 50,
                Speed = 80,
                ImageUrl = ""
            },
            new Monster
            {
                Id = 2,
                Name = "monster-2",
                Attack = 70,
                Defense = 20,
                Hp = 100,
                Speed = 40,
                ImageUrl = ""
            },
            new Monster
            {
                Id = 3,
                Name = "monster-3",
                Attack = 40,
                Defense = 20,
                Hp = 50,
                Speed = 10,
                ImageUrl = ""
            },
            new Monster
            {
                Id = 4,
                Name = "monster-4",
                Attack = 70,
                Defense = 20,
                Hp = 50,
                Speed = 40,
                ImageUrl = ""
            },
            new Monster
            {
                Id = 5,
                Name = "monster-5",
                Attack = 40,
                Defense = 20,
                Hp = 100,
                Speed = 40,
                ImageUrl = ""
            },
            new Monster
            {
                Id = 6,
                Name = "monster-6",
                Attack = 10,
                Defense = 10,
                Hp = 100,
                Speed = 80,
                ImageUrl = ""
            },
            new Monster
            {
                Id = 7,
                Name = "monster-7",
                Attack = 60,
                Defense = 10,
                Hp = 150,
                Speed = 40,
                ImageUrl = ""
            }
        };
    }
}