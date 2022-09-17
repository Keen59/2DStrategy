using System.Collections;
using System.Collections.Generic;
using AStar._Scripts.Grid;
using AStar._Scripts.Tiles;
using UnityEngine;
using AStar._Scripts.Units;


public class EnemySoldier : Enemy
{
    public int damage;
    public float speed;
    public NodeBase currentNode;
 

    public override void GiveDamage(int damage)
    {

    }

    public override void AnimationTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public override void OnClick()
    {

        Publisher.Publish += GetDamage;
        AnimationTrigger("GetDamage");

    }

    public override void GetDamage(int damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            Publisher.Publish -= GetDamage;

            currentNode.Walkable = true;
            //ProductionManager.Instance.SelectedSoldier.unit.CurrentEnemy = null;
            Destroy(gameObject,0.1f);

        }
    }

  

  

}