using Lib.Repository.Entities;

namespace Lib.Repository.Services;

public static class BattleService
{
    public static Battle StartBattle (Battle battle)
    {

        int? MonsterGoFirstID = 0;
        // we need to evaluate speed, attack and more.
        if ( battle.MonsterARelation.Speed > battle.MonsterBRelation.Speed )
            {
                MonsterGoFirstID = battle.MonsterARelation.Id;
            }
        else if (battle.MonsterARelation.Speed == battle.MonsterBRelation.Speed)
            {
                // if speed is equal, we must evaluate by higher Attack
                if (battle.MonsterARelation.Attack > battle.MonsterBRelation.Attack  )
                {
                      MonsterGoFirstID = battle.MonsterARelation.Id;
                }
                // what will happen if we choose same monster?, because we do not have any rule if speed is lower or equal and attack is equal
                else {
                      MonsterGoFirstID = battle.MonsterBRelation.Id;
                }
                MonsterGoFirstID = battle.MonsterBRelation.Id;
            }

        // now we are ready to start the battle
        bool StartBattle = true;
        //Get Monster A Damage
        int MonsterDamageA =   battle.MonsterARelation.Attack <= battle.MonsterARelation.Defense ? 1 :  battle.MonsterARelation.Attack - battle.MonsterARelation.Defense;
        //Get Monster B Damage
        int MonsterDamageB =   battle.MonsterBRelation.Attack <= battle.MonsterBRelation.Defense ? 1 :  battle.MonsterBRelation.Attack - battle.MonsterBRelation.Defense;
        
        
        // Counter for HP A and B
        int MonsterAHP = battle.MonsterARelation.Hp;
        int MonsterBHP = battle.MonsterBRelation.Hp;        

        while (StartBattle)
            {
                if (  MonsterGoFirstID == battle.MonsterA  )
                {
                        MonsterBHP -= MonsterDamageA;
                        if ( MonsterBHP <= 0 )
                            {
                                battle.WinnerRelation = battle.MonsterARelation;
                                battle.Winner = battle.MonsterA;
                                break;
                            }

                        MonsterAHP -= MonsterDamageB;
                        if ( MonsterAHP <= 0 )
                            {
                                battle.WinnerRelation = battle.MonsterBRelation;
                                battle.Winner = battle.MonsterB;
                                break;
                            }
                }
                else {
                        MonsterAHP -= MonsterDamageB;
                        if ( MonsterAHP <= 0 )
                            {
                                battle.WinnerRelation = battle.MonsterBRelation;
                                battle.Winner = battle.MonsterB;
                                break;
                            }
                        MonsterBHP -= MonsterDamageA;
                        if ( MonsterBHP <= 0 )
                            {
                                battle.WinnerRelation = battle.MonsterARelation;
                                battle.Winner = battle.MonsterA;
                                break;
                            }


                }

            }
            return battle;
    }
}