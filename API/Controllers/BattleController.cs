using Lib.Repository.Entities;
using Lib.Repository.Repository;
using Lib.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class BattleController : BaseApiController
{
    private readonly IBattleOfMonstersRepository _repository;

    public BattleController ( IBattleOfMonstersRepository repository)
    {
        this._repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        IEnumerable<Battle> battles = await _repository.Battles.GetAllAsync();
        return Ok(battles);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Add([FromBody] Battle battle)
    {
        // Check is monsters are Null
        if (( battle.MonsterA is null ) || (battle.MonsterB is null))
        
        {
            return BadRequest("Missing ID");
        }
        // Check is monsters exists
        if ((   await _repository.Monsters.FindAsync(battle.MonsterA) is null ) ||
             ( await _repository.Monsters.FindAsync(battle.MonsterB) is null ))
             {
                return BadRequest ($"Monster Not Found"  );

             }
        // TODO get Max Battle ID from DB
            //battle.Id = 


        // Get monsters by ID
        battle.MonsterARelation = await _repository.Monsters.FindAsync(battle.MonsterA);
        battle.MonsterBRelation = await _repository.Monsters.FindAsync(battle.MonsterB);        

  

        battle= BattleService.StartBattle(battle);   

        await _repository.Battles.AddAsync(battle);
        await _repository.Save();
        return Ok(battle);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Remove(int id)
    {
        if ( await _repository.Battles.FindAsync(id) is null )
        {
            return NotFound($"The Battle with ID = {id} not found.");
        }
        await _repository.Battles.RemoveAsync(id);
        await _repository.Save();
        return Ok();
    }
}


