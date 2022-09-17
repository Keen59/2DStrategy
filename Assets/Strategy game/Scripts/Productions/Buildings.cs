using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AStar._Scripts.Grid;
using AStar._Scripts.Tiles;
using UnityEngine;



public abstract class Buildings : Production
{
    public float spawnTime;
    private Vector3 mousePos;
    public int _Height, _Width;
    [HideInInspector] public bool MoveAble=false;
    [HideInInspector] public bool InTouchablePlace = false;
    public Color _BuildingColor;
    public List<NodeBase> UnitSpawnPoints;
    [HideInInspector] public Soldier CurrentSpawningSoldier;
    [HideInInspector] public int perimeter;
    public override void GiveDamage(int damage)
    {
        
    }
    
    public override void AnimationTrigger(string damage)
    {
        
    }

    public override void GetDamage(int damage)
    {
        
    }

    public void SetNeighborNodes(NodeBase baseNode)
    {
        if (baseNode.Walkable)
            UnitSpawnPoints.Add(baseNode);
    }

    public override void OnClick()
    {
        EventManager.Instance.Activation(gameObject);
        ProductionManager.Instance.SelectedBuilding = this;
    }
    public void SetProperties()
    {
        gameObject.SetActive(true);
        MoveAble = true;
    }
    private void StopMovement()
    {
        MoveAble = false;

        image.color = _BuildingColor;
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
               
                        

                       
                    if ((x>=transform.position.x&&x<=transform.position.x+_Width-1)&&( y >= transform.position.y && y <= transform.position.y + _Height-1))
                    {
                        obj.Obstacle(false);

                    }
                }

            }
        }

        perimeter = (_Height + 2) * (_Width + 2) - (_Width * _Height);


    }
    private void OnMouseDown()
    {
        if (!MoveAble)
        {
            OnClick();
        }
    }
    void Update()
    {
        if (MoveAble)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y),-0.1f);
            if (Mathf.RoundToInt(transform.position.x)  < 0 || Mathf.RoundToInt(transform.position.x) + _Width > GridManager.Instance._scriptableSquareGrid._gridWidth || Mathf.RoundToInt(transform.position.y) < 0 || Mathf.RoundToInt(transform.position.y) + _Height > GridManager.Instance._scriptableSquareGrid._gridHeight)
            {
                image.color = Color.red;
            }
            else
            {
                if (!InTouchablePlace)
                {
                  image.color = _BuildingColor;
                  Debug.Log("aaaaaaaaaaaaaaaaaaaasss");

                }
            }

            if (Input.GetMouseButtonDown(0)&& !InTouchablePlace)
            {
                if (Mathf.RoundToInt(transform.position.x) >= 0 && Mathf.RoundToInt(transform.position.x) + _Width <= GridManager.Instance._scriptableSquareGrid._gridWidth && Mathf.RoundToInt(transform.position.y) >= 0 && Mathf.RoundToInt(transform.position.y) + _Height <= GridManager.Instance._scriptableSquareGrid._gridHeight)
                {
                    StopMovement();
                }
            }
        }

   
    }
}
