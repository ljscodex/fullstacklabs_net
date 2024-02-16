using System.Diagnostics;
using API.Controllers;
using API.Test.Fixtures;
using FluentAssertions;
using Lib.Repository.Entities;
using Lib.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Test;

public class MonsterControllerTests
{
    private readonly Mock<IBattleOfMonstersRepository> _repository;

    public MonsterControllerTests()
    {
        this._repository = new Mock<IBattleOfMonstersRepository>();
    }
    
    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfMonsters()
    {
        Monster[] monsters = MonsterFixture.GetMonstersMock().ToArray();

        this._repository
            .Setup(x => x.Monsters.GetAllAsync())
            .ReturnsAsync(monsters);

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.GetAll();
        OkObjectResult objectResults = (OkObjectResult) result;
        objectResults?.Value.Should().BeOfType<Monster[]>();
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsOneMonsterById()
    {
        const int id = 1;
        Monster[] monsters = MonsterFixture.GetMonstersMock().ToArray();

        Monster monster = monsters[0];
        this._repository
            .Setup(x => x.Monsters.FindAsync(id))
            .ReturnsAsync(monster);

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Find(id);
        OkObjectResult objectResults = (OkObjectResult)result;
        objectResults?.Value.Should().BeOfType<Monster>();
    }

    [Fact]
    public async Task Get_OnNoMonsterFound_Returns404()
    {
        const int id = 123;

        this._repository
            .Setup(x => x.Monsters.FindAsync(id))
            .ReturnsAsync(() => null);

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Find(id);
        NotFoundObjectResult objectResults = (NotFoundObjectResult)result;
        result.Should().BeOfType<NotFoundObjectResult>();
        Assert.Equal($"The monster with ID = {id} not found.", objectResults.Value);
    }

    [Fact]
    public async Task Post_OnSuccess_CreateMonster()
    {
        Monster m = new Monster()
        {
            Name = "Monster Test",
            Attack = 50,
            Defense = 40,
            Hp = 80,
            Speed = 60,
            ImageUrl = ""
        };

        this._repository
            .Setup(x => x.Monsters.AddAsync(m));

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Add(m);
        OkObjectResult objectResults = (OkObjectResult)result;
        objectResults?.Value.Should().BeOfType<Monster>();
    }

    [Fact]
    public async Task Put_OnSuccess_UpdateMonster()
    {
        const int id = 1;
        Monster[] monsters = MonsterFixture.GetMonstersMock().ToArray();

        Monster m = new Monster()
        {
            Name = "Monster Update"
        };

        this._repository
            .Setup(x => x.Monsters.FindAsync(id))
            .ReturnsAsync(monsters[0]);

        this._repository
           .Setup(x => x.Monsters.Update(id, m));

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Update(id, m);
        OkResult objectResults = (OkResult)result;
        objectResults.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Put_OnNoMonsterFound_Returns404()
    {
        const int id = 123;

        Monster m = new Monster()
        {
            Name = "Monster Update"
        };

        this._repository
            .Setup(x => x.Monsters.FindAsync(id))
            .ReturnsAsync(() => null);

        this._repository
           .Setup(x => x.Monsters.Update(id, m));

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Update(id, m);
        NotFoundObjectResult objectResults = (NotFoundObjectResult)result;
        result.Should().BeOfType<NotFoundObjectResult>();
        Assert.Equal($"The monster with ID = {id} not found.", objectResults.Value);
    }


    [Fact]
    public async Task Delete_OnSuccess_RemoveMonster()
    {
        const int id = 1;
        Monster[] monsters = MonsterFixture.GetMonstersMock().ToArray();

        this._repository
            .Setup(x => x.Monsters.FindAsync(id))
            .ReturnsAsync(monsters[0]);

        this._repository
           .Setup(x => x.Monsters.RemoveAsync(id));

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Remove(id);
        OkResult objectResults = (OkResult)result;
        objectResults.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Delete_OnNoMonsterFound_Returns404()
    {
        const int id = 123;

        this._repository
            .Setup(x => x.Monsters.FindAsync(id))
            .ReturnsAsync(() => null);

        this._repository
           .Setup(x => x.Monsters.RemoveAsync(id));

        MonsterController sut = new MonsterController(this._repository.Object);

        ActionResult result = await sut.Remove(id);
        NotFoundObjectResult objectResults = (NotFoundObjectResult)result;
        result.Should().BeOfType<NotFoundObjectResult>();
        Assert.Equal($"The monster with ID = {id} not found.", objectResults.Value);
    }

    [Fact]
    public async Task Post_OnSuccess_ImportCsvToMonster()
    {
        // @TODO missing implementation
    }

}