using System;
using System.Collections;
using System.Collections.Generic;
using AStar._Scripts.Tiles;
using UnityEngine;



public class Barracks : Buildings
{
    public BoxCollider2D DetectionCollider;
    public Barracks()
   {

   }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Node"&&!MoveAble)
        {
            NodeBase node = other.GetComponent<NodeBase>();
            if (!UnitSpawnPoints.Find(x=>x== other.GetComponent<NodeBase>()) && node.Walkable&& UnitSpawnPoints.Count<perimeter)
            {
              SetNeighborNodes(other.GetComponent<NodeBase>());
            }
      
        }
        else if (other.gameObject.layer == 8 && MoveAble)
        {
            InTouchablePlace = true;
            image.color = Color.red;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8&&MoveAble)
        {
            InTouchablePlace = true;
            image.color = Color.red;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && MoveAble)
        {
            InTouchablePlace = false;

            image.color = _BuildingColor;
        }
    }
    public new void GetDamage(int damage)
       {
         Health-=damage;
         SetText(damage);
       }

    public override void SetText(int damageSize)
    {
        HPText.text = "-" + damageSize;
        StartCoroutine(SetTextNull());
    }

    IEnumerator SetTextNull()
    {
        yield return new WaitForSeconds(0.2f);
        HPText.text = "";
    }
}
