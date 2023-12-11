using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiScript : EnemyScript
{
    protected override void Awake()
    {
        base.Awake();
        type = Type.Sushi;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldAttack = false;

            attackTimer = 0;
        }
    }
}
