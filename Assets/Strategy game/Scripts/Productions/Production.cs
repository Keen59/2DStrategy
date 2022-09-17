using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


public abstract class Production : MonoBehaviour
{
    public string itemName;
    public string Description;
    public int[,] size;
    public int Health;
    public SpriteRenderer image;
    public Text HPText;
    public bool Selected=false;
    public Animator animator;
    public void SpriteOutLine(Color color, float Thickness)
    {
        image.material.SetColor("_SolidOutline", color);
        image.material.SetFloat("_Thickness", Thickness);
    }
    public abstract void GetDamage(int damage);
   public abstract void GiveDamage(int damage);
   public abstract void AnimationTrigger(string triggerName);
    public abstract void OnClick();
    public abstract void SetText(int damageSize);
}
