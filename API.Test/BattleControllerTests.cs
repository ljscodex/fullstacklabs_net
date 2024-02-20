using API.Controllers;
using API.Test.Fixtures;
using FluentAssertions;
using Lib.Repository.Entities;
using Lib.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Test;

public class BattleControllerTests
{
    private readonly Mock<IBattleOfMonstersRepository> _repository;

    public BattleControllerTests()
    {
        this._repository = new Mock<IBattleOfMonstersRepository>();
    }

    [Fact]
    public async void Get_OnSuccess_ReturnsListOfBattles()
    {
        this._repository
            .Setup(x => x.Battles.GetAllAsync())
            .ReturnsAsync(BattlesFixture.GetBattlesMock());

        BattleController sut = new BattleController( this._repository.Object);
        ActionResult result = await sut.GetAll();
        OkObjectResult objectResults = (OkObjectResult) result;
        objectResults?.Value.Should().BeOfType<Battle[]>();
    }
    
    [Fact]
    public async Task Post_BadRequest_When_StartBattle_With_nullMonster()
    {
        Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
        Battle b = new Battle()
        {
            MonsterA = null,
            MonsterB = monstersMock[1].Id
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));

        int? idMonsterA = null;
        this._repository
            .Setup(x => x.Monsters.FindAsync(idMonsterA))
            .ReturnsAsync(() => null);

        int? idMonsterB = monstersMock[1].Id;
        Monster monsterB = monstersMock[1];

        this._repository
            .Setup(x => x.Monsters.FindAsync(idMonsterB))
            .ReturnsAsync(monsterB);

        BattleController sut = new BattleController(this._repository.Object);

        ActionResult result = await sut.Add(b);
        BadRequestObjectResult objectResults = (BadRequestObjectResult) result;
        result.Should().BeOfType<BadRequestObjectResult>();
        Assert.Equal("Missing ID", objectResults.Value);
    }
    
    [Fact]
    public async Task Post_OnNoMonsterFound_When_StartBattle_With_NonexistentMonster()
    {
        int MonsterBid = 1;
        int MonsterAid = 123;

        Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
        Battle b = new Battle()
        {
            MonsterA = MonsterAid,
            MonsterB = MonsterBid
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));

         this._repository
            .Setup(x => x.Monsters.FindAsync(MonsterAid))
            .ReturnsAsync(() => null);


        Monster monsterB = monstersMock[MonsterBid];

        this._repository
            .Setup(x => x.Monsters.FindAsync(MonsterBid))
            .ReturnsAsync(monsterB);

        BattleController sut = new BattleController(this._repository.Object);

        ActionResult result = await sut.Add(b);
        BadRequestObjectResult objectResults = (BadRequestObjectResult) result;
        result.Should().BeOfType<BadRequestObjectResult>();
        Assert.Equal("Monster Not Found", objectResults.Value);        
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterAWinning()
    {
       Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
       Monster monsterA = monstersMock[4];

        this._repository
        .Setup(x => x.Monsters.FindAsync(monsterA.Id))
        .ReturnsAsync(monsterA);

       Monster monsterB = monstersMock[0];

        this._repository
            .Setup(x => x.Monsters.FindAsync(monsterB.Id))
            .ReturnsAsync(monsterB);

        Battle b = new Battle()
        {
            MonsterA = monsterA.Id,
            MonsterB = monsterB.Id,
            Id = 11
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));



        BattleController sut = new BattleController(this._repository.Object);
        ActionResult result = await sut.Add(b);
        OkObjectResult objectResults = (OkObjectResult)result;
        result.Should().BeOfType<OkObjectResult>();
        objectResults?.Value.Should().BeOfType<Battle>();
        Assert.Equal( monsterA.Id,((Lib.Repository.Entities.Battle)objectResults.Value).Winner);
    }


    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterBWinning()
    {
       Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
       Monster monsterA = monstersMock[0];

        this._repository
        .Setup(x => x.Monsters.FindAsync(monsterA.Id))
        .ReturnsAsync(monsterA);

       Monster monsterB = monstersMock[4];

        this._repository
            .Setup(x => x.Monsters.FindAsync(monsterB.Id))
            .ReturnsAsync(monsterB);

        Battle b = new Battle()
        {
            MonsterA = monsterA.Id,
            MonsterB = monsterB.Id,
            Id = 10
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));



        BattleController sut = new BattleController(this._repository.Object);
        ActionResult result = await sut.Add(b);
        OkObjectResult objectResults = (OkObjectResult)result;
        result.Should().BeOfType<OkObjectResult>();
        objectResults?.Value.Should().BeOfType<Battle>();
        Assert.Equal( monsterB.Id,((Lib.Repository.Entities.Battle)objectResults.Value).Winner);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterAWinning_When_TheirSpeedsSame_And_MonsterA_Has_Higher_Attack()
    {
          Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
       Monster monsterA = monstersMock[3];

        this._repository
        .Setup(x => x.Monsters.FindAsync(monsterA.Id))
        .ReturnsAsync(monsterA);

       Monster monsterB = monstersMock[4];

        this._repository
            .Setup(x => x.Monsters.FindAsync(monsterB.Id))
            .ReturnsAsync(monsterB);

        // check same speed
        Assert.Equal( monsterA.Speed, monsterB.Speed);
        // check A higher Attack than B
        Assert.True ( monsterA.Attack > monsterB.Attack);


        Battle b = new Battle()
        {
            MonsterA = monsterA.Id,
            MonsterB = monsterB.Id,
            Id = 10
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));



        BattleController sut = new BattleController(this._repository.Object);
        ActionResult result = await sut.Add(b);
        OkObjectResult objectResults = (OkObjectResult)result;
        result.Should().BeOfType<OkObjectResult>();
        objectResults?.Value.Should().BeOfType<Battle>();
        Assert.Equal( monsterA.Id,((Lib.Repository.Entities.Battle)objectResults.Value).Winner);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterBWinning_When_TheirSpeedsSame_And_MonsterB_Has_Higher_Attack()
    {
       Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
       Monster monsterA = monstersMock[4];

        this._repository
        .Setup(x => x.Monsters.FindAsync(monsterA.Id))
        .ReturnsAsync(monsterA);

       Monster monsterB = monstersMock[3];

        this._repository
            .Setup(x => x.Monsters.FindAsync(monsterB.Id))
            .ReturnsAsync(monsterB);


        // check same speed
        Assert.Equal( monsterA.Speed, monsterB.Speed);
        // check B higher Attack than A
        Assert.True ( monsterB.Attack > monsterA.Attack);

        Battle b = new Battle()
        {
            MonsterA = monsterA.Id,
            MonsterB = monsterB.Id,
            Id = 10
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));



        BattleController sut = new BattleController(this._repository.Object);
        ActionResult result = await sut.Add(b);
        OkObjectResult objectResults = (OkObjectResult)result;
        result.Should().BeOfType<OkObjectResult>();
        objectResults?.Value.Should().BeOfType<Battle>();
        Assert.Equal( monsterB.Id,((Lib.Repository.Entities.Battle)objectResults.Value).Winner);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterAWinning_When_TheirDefensesSame_And_MonsterA_Has_Higher_Speed()
    {
            Monster[] monstersMock = MonsterFixture.GetMonstersMock().ToArray();
        
       Monster monsterA = monstersMock[3];

        this._repository
        .Setup(x => x.Monsters.FindAsync(monsterA.Id))
        .ReturnsAsync(monsterA);

       Monster monsterB = monstersMock[2];

        this._repository
            .Setup(x => x.Monsters.FindAsync(monsterB.Id))
            .ReturnsAsync(monsterB);

        //Check Defense (Same)
        Assert.Equal (monsterA.Defense , monsterB.Defense);
        //Check monsterA > speed
        Assert.True( monsterA.Speed > monsterB.Speed);
        

        Battle b = new Battle()
        {
            MonsterA = monsterA.Id,
            MonsterB = monsterB.Id,
            Id = 10
        };

        this._repository.Setup(x => x.Battles.AddAsync(b));



        BattleController sut = new BattleController(this._repository.Object);
        ActionResult result = await sut.Add(b);
        OkObjectResult objectResults = (OkObjectResult)result;
        result.Should().BeOfType<OkObjectResult>();
        objectResults?.Value.Should().BeOfType<Battle>();
        Assert.Equal( monsterA.Id,((Lib.Repository.Entities.Battle)objectResults.Value).Winner);
    }

    [Fact]
    public async Task Delete_OnSuccess_RemoveBattle()
    {
   

        Battle[] battles = BattlesFixture.GetBattlesMock().ToArray();

        int id = battles[0].Id  ?? 0;

       this._repository
          .Setup(x => x.Battles.RemoveAsync(id));

        Battle battle = battles[0];

        this._repository
            .Setup(x => x.Battles.FindAsync(id))
            .ReturnsAsync(battle);

        BattleController sut = new BattleController(this._repository.Object);

        ActionResult result = await sut.Remove(id);
        OkResult objectResults = (OkResult)result;
        result.Should().BeOfType<OkResult>();
 }

    [Fact]
    public async Task Delete_OnNoBattleFound_Returns404()
    {
        // @TODO missing implementation
        const int id = 123;

        this._repository
            .Setup(x => x.Battles.FindAsync(id))
            .ReturnsAsync(() => null);

        this._repository
           .Setup(x => x.Battles.RemoveAsync(id));

        BattleController sut = new BattleController(this._repository.Object);

        ActionResult result = await sut.Remove(id);
        NotFoundObjectResult objectResults = (NotFoundObjectResult)result;
        result.Should().BeOfType<NotFoundObjectResult>();
        Assert.Equal($"The Battle with ID = {id} not found.", objectResults.Value);
    }
}
