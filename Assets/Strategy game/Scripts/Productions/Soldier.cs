using System.Collections;
using System.Collections.Generic;
using AStar._Scripts.Grid;
using UnityEngine;
using AStar._Scripts.Units;


public class Soldier : Production
{
    public int damage;
    public float speed;
    public Unit unit;
    


    public override void GiveDamage(int damage)
    {

    }

    public override void AnimationTrigger(string triggerName)
    {
        
    }

    public override void GetDamage(int damage)
    {
        Health -= damage;
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
    public override void OnClick()
     {
        if (!Selected)
        {
            GridManager.Instance.Selected_spawnedPlayer = unit;
            GridManager.Instance._playerNodeBase = unit.CurrentnodeBase;
            unit.CurrentnodeBase.SpriteOutLine(Color.yellow, 30);
            ProductionManager.Instance.SelectedSoldier = this;
            Selected = true;
            transform.localScale = Vector3.one*.8f;
        }
        else
        {
            EventManager.Instance.UnitOnClick -= OnClick;

            unit.CurrentnodeBase.SpriteOutLine(Color.yellow, 0);
            ProductionManager.Instance.SelectedSoldier = null;
            transform.localScale = Vector3.one * 1;

            Selected = false;
        }

    }
}