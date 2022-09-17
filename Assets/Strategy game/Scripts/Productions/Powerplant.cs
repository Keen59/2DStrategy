using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerplant : Buildings
{
   public float damage;
  
   public new void GiveDamage(int damage)
    {
        
    }
   public new void GetDamage(int damage)
   {
       Health -= damage;
       SetText(damage);
   }
   private void OnTriggerStay2D(Collider2D other)
   {

      if (other.gameObject.layer == 8 && MoveAble)
        {
           InTouchablePlace = true;
           image.color = Color.red;
       }
   }
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.gameObject.layer == 8 && MoveAble)
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
