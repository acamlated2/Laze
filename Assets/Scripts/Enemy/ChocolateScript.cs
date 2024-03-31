using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateScript : EnemyScript
{
    public bool haveShield = true;
    
    protected override void Awake()
    {
        base.Awake();
        type = Type.Chocolate;

        var gameControllerScript = GameController.GetComponent<GameControllerScript>();
        gameControllerScript.chocolates.Add(gameObject);

        health = 50;
    }

    protected override void FixedUpdate()
    {
        if (Player == null)
        {
            return;
        }
        
        // if (haveShield)
        // {
        //     HandleShieldedMovements();
        // }
        // else
        // {
        //     agent.SetDestination(player.transform.position);
        // }
    }

    private void HandleShieldedMovements()
    {
        Vector3 desiredPosition = Player.transform.position - (Player.transform.position - 
                                                               transform.position).normalized * panicDistance;
        
        agent.SetDestination(desiredPosition);
    }

    protected override void Die()
    {
        base.Die();
        GameController.GetComponent<GameControllerScript>().chocolates.Remove(gameObject);
    }
}
