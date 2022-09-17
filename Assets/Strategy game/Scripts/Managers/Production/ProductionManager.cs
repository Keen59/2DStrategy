using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbilityFactory;
using AStar._Scripts.Grid;
using AStar._Scripts.Tiles;
using AStar._Scripts.Units;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    public static ProductionManager Instance { get; private set; }
    public Soldier SelectedSoldier;

    public Buildings SelectedBuilding;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnUnit(Soldier sol)
    {
        SelectedBuilding.CurrentSpawningSoldier = PoolManager.instance.Get(sol.gameObject.tag).GetComponent<Soldier>();

        FactoryCalls.GetAbility("Spawn").Process();

    }
    public void SpawnBuildings(Buildings building)
    {
        Buildings spawnedBuilding;
        if (building.gameObject.tag == "Barracks")
        {
            spawnedBuilding = PoolManager.instance.Get(building.gameObject.tag).GetComponent<Barracks>();

        }
        else
        {
            spawnedBuilding = PoolManager.instance.Get(building.gameObject.tag).GetComponent<Powerplant>();

        }
        spawnedBuilding.SetProperties();

    }

    public void SelectMovePosition(NodeBase target)
    {
        if (target.Walkable)
        {
            target.SpriteOutLine(Color.green, 30);
            target.OnHoverTileCall();
            GridManager.Instance.enemy = null;

        }
    }
    public void AttackSelectedEnemy(Enemy enemy, NodeBase target)
    {
        if (target.Walkable)
        {

            enemy.transform.localScale = Vector3.one * .8f;
            GridManager.Instance.enemy = enemy;
            target.OnHoverTileCall();
        }
    }
    public void AttackSelectedBuilding(EnemyBuilding enemy)
    {

        var nearestNodeDisctance = float.MaxValue;
        NodeBase nearestNode = null;
        foreach (var item in enemy.UnitSpawnPoints)
        {
            float currentDistance = Vector2.Distance(SelectedSoldier.transform.position, item.transform.position);
            if (currentDistance < nearestNodeDisctance)
            {
                nearestNodeDisctance = currentDistance;
                nearestNode = item;
            }

        }
        EnemyBuilding Enemybuilding = enemy;
        GridManager.Instance.enemy = Enemybuilding;

        nearestNode.OnHoverTileCall();
    }
    public void spawnEnemyBarrack()
    {
        List<NodeBase> currentNodeBase = new List<NodeBase>();

        var _MapHeight = GridManager.Instance._scriptableSquareGrid._gridHeight;
        var _MapWidth = GridManager.Instance._scriptableSquareGrid._gridWidth;
        for (int i = 0; i < GridManager.Instance.Tiles.Count; i++)
        {
            for (int x = 0; x < _MapWidth; x++)
            {
                for (int y = 0; y < _MapHeight; y++)
                {
                    var obj = GridManager.Instance.Tiles[new Vector2(x, y)];

                    if (obj.Walkable)
                    {
                        currentNodeBase.Add(obj);

                    }

                }

            }
        }

        EnemyBuilding enemyBuildpowerPlant = PoolManager.instance.Get("EnemyPowerPlant").GetComponent<EnemyBuilding>();

        CreatingEnemyBuilding("EnemyBarrack", currentNodeBase);
        CreatingEnemyBuilding("EnemyPowerPlant", currentNodeBase);
        SpawnEnemySoldier(currentNodeBase);


    }

    public void SpawnEnemySoldier(List<NodeBase> nodebases)
    {
        var nodeBaseHold = nodebases[Random.Range(0, nodebases.Count)];

        if (nodeBaseHold.Walkable)
        {
            GameObject enemy = PoolManager.instance.Get("Enemies");
            enemy.SetActive(true);
            enemy.transform.position = new Vector3(nodeBaseHold.Coords.Pos.x, nodeBaseHold.Coords.Pos.y, -.1f); 
            enemy.GetComponent<Enemy>().currentNode = nodeBaseHold;
            nodeBaseHold.Walkable = false;
        }
        else
        {

            SpawnEnemySoldier(nodebases);

        }
    }


    public void CreatingEnemyBuilding(string creatingBuildtag, List<NodeBase> currentNodeBase)
    {
        EnemyBuilding enemyBuild = PoolManager.instance.Get(creatingBuildtag).GetComponent<EnemyBuilding>();

        NodeBase SelectedNode;
        if (creatingBuildtag == "EnemyBarrack")
        {
            SelectedNode = currentNodeBase.Find(xnodebase => xnodebase.Coords.Pos.x == (GridManager.Instance._scriptableSquareGrid._gridWidth / 2) - 2 && xnodebase.Coords.Pos.y == GridManager.Instance._scriptableSquareGrid._gridHeight - 4);

        }
        else
        {
            SelectedNode = currentNodeBase.Find(xnodebase => xnodebase.Coords.Pos.x == (GridManager.Instance._scriptableSquareGrid._gridWidth / 2) - 1 && xnodebase.Coords.Pos.y == GridManager.Instance._scriptableSquareGrid._gridHeight - 7);

        }

        enemyBuild.transform.position = new Vector3(SelectedNode.Coords.Pos.x, SelectedNode.Coords.Pos.y,-.1f);

        enemyBuild.StopMovement();







    }


    private void Start()
    {
        PoolManager.instance.GeneratePool();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && SelectedSoldier != null)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);



            if (hit.transform.tag == "Node")
            {
                SelectMovePosition(hit.transform.GetComponent<NodeBase>());

            }
            else if (hit.transform.tag == "Enemies")
            {

                Enemy enemy = hit.transform.GetComponent<Enemy>();
                enemy.currentNode.Walkable = true;
                AttackSelectedEnemy(enemy, enemy.currentNode);

            }
            else if (hit.transform.tag == "EnemyBarrack")
            {

                EnemyBuilding building = hit.transform.GetComponent<EnemyBuilding>();

                AttackSelectedBuilding(building);

            }
            else if (hit.transform.tag == "EnemyPowerPlant")
            {

                EnemyBuilding building = hit.transform.GetComponent<EnemyBuilding>();

                AttackSelectedBuilding(building);

            }
        }
    }
}
