using System.Collections.Generic;
using System.Linq;
using AStar._Scripts.Tiles;
using AStar._Scripts.Grid.Scriptables;
using AStar._Scripts.Units;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AStar._Scripts.Grid
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance;


        [SerializeField] private Unit _unitPrefab;
        public ScriptableSquareGrid _scriptableSquareGrid;
        [SerializeField] private bool _drawConnections;
        [SerializeField] private Camera camTransform;

        public Dictionary<Vector2, NodeBase> Tiles { get; private set; }

        [HideInInspector] public NodeBase _playerNodeBase, _goalNodeBase;
        [HideInInspector] public Unit Selected_spawnedPlayer;
        [HideInInspector] public Enemy enemy;

        void Awake() => Instance = this;

        private void Start()
        {
            Tiles = _scriptableSquareGrid.GenerateGrid();
           ProductionManager.Instance.spawnEnemyBarrack();
           foreach (var tile in Tiles.Values) tile.CacheNeighbors();

            //SpawnUnits();
            NodeBase.OnHoverTile += OnTileHover;
            camTransform.transform.position= new Vector3((float)_scriptableSquareGrid._gridWidth / 2 - 0.5f, (float)_scriptableSquareGrid._gridHeight / 2 - 0.5f, -10);
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float target = (float)_scriptableSquareGrid._gridWidth / (float)_scriptableSquareGrid._gridHeight;
            if (screenRatio >= target)
            {
                camTransform.orthographicSize = (float)_scriptableSquareGrid._gridHeight / 2;

            }
            else
            {
                float difSize = target / screenRatio;
                camTransform.orthographicSize = (float)_scriptableSquareGrid._gridHeight / 2 * difSize;
            }
        }

       

        private void OnDestroy() => NodeBase.OnHoverTile -= OnTileHover;

        private void OnTileHover(NodeBase nodeBase)
        {
            if (Selected_spawnedPlayer != null)
            {

                _goalNodeBase = nodeBase;

            

                _playerNodeBase = Selected_spawnedPlayer.CurrentNodeBase();

                foreach (var t in Tiles.Values) t.RevertTile();

                var path = Pathfinding.FindPath(_playerNodeBase, _goalNodeBase);
                var enemySelected=false;

                if (enemy!=null)
                {
                    enemySelected = true;
                }
                
                Selected_spawnedPlayer.SetPath(path, enemySelected);

            }

        }


        public NodeBase GetTileAtPosition(Vector2 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || !_drawConnections) return;
            Gizmos.color = Color.red;
            foreach (var tile in Tiles)
            {
                if (tile.Value.Connection == null) continue;
                Gizmos.DrawLine((Vector3)tile.Key + new Vector3(0, 0, -1), (Vector3)tile.Value.Connection.Coords.Pos + new Vector3(0, 0, -1));
            }
        }
    }
}