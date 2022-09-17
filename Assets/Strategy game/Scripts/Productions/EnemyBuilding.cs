using System.Collections;
using System.Collections.Generic;
using AStar._Scripts.Grid;
using AStar._Scripts.Tiles;
using UnityEngine;
using AStar._Scripts.Units;


public class EnemyBuilding : Enemy
{

    public List<NodeBase> UnitSpawnPoints;
    public int _Height, _Width;

    [HideInInspector] public int perimeter;
  
    public void SetNeighborNodes(NodeBase baseNode)
    {
        if (baseNode.Walkable)
            UnitSpawnPoints.Add(baseNode);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Node" && UnitSpawnPoints.Count < perimeter)
        {
            NodeBase node = other.GetComponent<NodeBase>();
            if (!UnitSpawnPoints.Find(x => x == other.GetComponent<NodeBase>()) && node.Walkable )
            {
                SetNeighborNodes(other.GetComponent<NodeBase>());
            }

        }
        
    }
    public void StopMovement()
    {
       
   

        var tiles = GridManager.Instance.Tiles;
        var _MapHeight = GridManager.Instance._scriptableSquareGrid._gridHeight;
        var _MapWidth = GridManager.Instance._scriptableSquareGrid._gridWidth;
        for (int i = 0; i < tiles.Count; i++)
        {
            for (int x = 0; x < _MapWidth; x++)
            {
                for (int y = 0; y < _MapHeight; y++)
                {
                    var obj = tiles[new Vector2(x, y)];




                    if ((x >= transform.position.x && x <= transform.position.x + _Width - 1) && (y >= transform.position.y && y <= transform.position.y + _Height - 1))
                    {
                        obj.Obstacle(false);

                    }
                }

            }
        }

        gameObject.SetActive(true);
        perimeter = (_Height + 2) * (_Width + 2) - (_Width * _Height);


    }




}