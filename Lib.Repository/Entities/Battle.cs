namespace Lib.Repository.Entities;

public class Battle
{
    public int? Id { get; set; }
    public int? MonsterA { get; set; }
    public int? MonsterB { get; set; }
    public int? Winner { get; set; }

    public Monster? MonsterARelation { get; set; }
    public Monster? MonsterBRelation { get; set; }
    public Monster? WinnerRelation { get; set; }
}