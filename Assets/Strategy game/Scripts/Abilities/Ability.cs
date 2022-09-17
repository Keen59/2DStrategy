using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AStar._Scripts.Grid;
using AStar._Scripts.Tiles;
using AStar._Scripts.Units;
using UnityEngine;


namespace AbilityFactory
{
    public abstract class Ability
    {
        public abstract string Name { get; }
        public abstract void Process();
    }
  
    public class AttackAbility : Ability
    {
        public override string Name => "Attack";

        public override void Process()
        {
            Soldier currentSelectedSoldier = ProductionManager.Instance.SelectedSoldier;
            Publisher.Instance.PublishTheEvent(currentSelectedSoldier.damage);
            
            currentSelectedSoldier.unit.CurrentEnemy.SpriteOutLine(Color.red,0);
            currentSelectedSoldier.AnimationTrigger("Attack");
            GridManager.Instance.enemy = null;
            Publisher.Publish -= currentSelectedSoldier.unit.CurrentEnemy.GetDamage;

            currentSelectedSoldier.unit.CurrentEnemy = null;

        }
    }
    public class MoveAbility : Ability
    {
        public override string Name => "Move";

        public override void Process()
        {

            Soldier currentSelectedSoldier = ProductionManager.Instance.SelectedSoldier;
            currentSelectedSoldier.unit.CurrentnodeBase.Obstacle(true);
            currentSelectedSoldier.unit.Move = true;
        }
    }
    public class GetDamageAbility : Ability
    {
        public override string Name => "GetDamage";

        public override void Process()
        {
            Production product = ProductionManager.Instance.SelectedSoldier;

            product.GetDamage(ProductionManager.Instance.SelectedSoldier.damage);
        }
    }
    public class Spawn : Ability
    {
        public override string Name => "Spawn";

        public override void Process()
        {
            Buildings product = ProductionManager.Instance.SelectedBuilding;
            int randomSpawnPoint = Random.Range(0, product.UnitSpawnPoints.Count);
            NodeBase basenode =product.UnitSpawnPoints[randomSpawnPoint];
            basenode.Walkable = false;
            product.UnitSpawnPoints.Remove(basenode);

            product.CurrentSpawningSoldier.unit.Init(basenode);
        }
    }

    public static class FactoryCalls
    {
        public static Ability GetAbility(string abilityType)
        {
            switch (abilityType)
            {
                case "Move":
                    return new MoveAbility();
                case "Attack":
                    return new AttackAbility();
                case "GetDamage":
                    return new GetDamageAbility();
                case "Spawn":
                    return new Spawn();
                default:
                    return null;
            }  
        }
    }
}
