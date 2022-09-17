using System.Collections.Generic;
using AStar._Scripts.Tiles;
using UnityEngine;

namespace AStar._Scripts.Grid.Scriptables {
    public abstract class ScriptableGrid : ScriptableObject {
        [SerializeField] protected NodeBase nodeBasePrefab;
        [SerializeField,Range(0,6)] private int _EnemyWeight = 3;
        public abstract Dictionary<Vector2, NodeBase> GenerateGrid();
        
        protected bool DecideIfObstacle() => Random.Range(1, 100) > _EnemyWeight;
    }
}
