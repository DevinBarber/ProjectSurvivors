using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSorrow : MonoBehaviour
{
    /* List of Attacks 
     Teleporting - On spawn destroy all nearby enemies **Undecided**
     4 Arms for different attack patterns
     Large cone attack using 2 hands other 2 shoot projectiles 
     Nova Attack
     Spawn orbs far from the boss, if they reach him he heals a % of maxhp
     Prob lock the player into an arena of some sort **Purple Mist**

     Movement Pattern
     
     */

    private enum State
    {
        Spawn,
        Move,
        Teleporting,
        NovaAttack,
        AttackPatternOne,
        AttackPatternTwo,
        SpawnHealingOrbs,
    }

    private State bossState;

    // Start is called before the first frame update
    void Start()
    {
        bossState = State.Spawn;
    }

    // Update is called once per frame
    void Update()
    {
        switch(bossState)
        {
            case State.Spawn:
                break;
            case State.Move:
                break;
            case State.Teleporting:
                break; 
            case State.NovaAttack:
                break;
            case State.AttackPatternOne:
                break;
            case State.AttackPatternTwo:
                break;
            case State.SpawnHealingOrbs:
                break;
        }
    }

    private IEnumerator SorrowSpawn()
    {
        //Get player position and lock player into arena
        //Get all enemies in pool and disable them -> Stop game timer
        //Then off player position or something similar choose game state to start in

        yield return null;
    }
}
