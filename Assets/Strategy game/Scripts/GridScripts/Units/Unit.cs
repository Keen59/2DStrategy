using System.Collections.Generic;
using AbilityFactory;
using AStar._Scripts.Grid;
using AStar._Scripts.Tiles;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace AStar._Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Soldier soldierUnit;
        [SerializeField] private List<NodeBase> CurrentPath;
        [HideInInspector] public NodeBase CurrentnodeBase;
        [HideInInspector] public NodeBase CurrentTargetDestination;
        [SerializeField] private int currentPos;
        public bool Move = false;
        [HideInInspector] public Enemy CurrentEnemy { get; set; }

        void Start()
        {

        }

        
        private void OnMouseDown()
        {
            if (!soldierUnit.Selected)
            {
                EventManager.Instance.UnitOnClick += soldierUnit.OnClick;
            }

            EventManager.Instance.unitOnClick();
        }

        public void Init(NodeBase nodeBase)
        {
            CurrentnodeBase = nodeBase;
            transform.position = nodeBase.transform.position;
            gameObject.SetActive(true);
        }
        public void SetPath(List<NodeBase> nodeBasePath, bool SelectedEnemy)
        {
            if (SelectedEnemy)
            {
                if (nodeBasePath.Count >1)
                {
                    CurrentEnemy = GridManager.Instance.enemy;
                   
                    if (CurrentTargetDestination != null)
                        CurrentTargetDestination.SpriteOutLine(Color.yellow, 0);

                    CurrentnodeBase.SpriteOutLine(Color.yellow, 0);

                    if (CurrentEnemy.tag == "Enemies")
                    {
                        CurrentEnemy.SpriteOutLine(Color.red, 30);

                        nodeBasePath[0].Walkable = false;
                        nodeBasePath.Remove(nodeBasePath[0]);
                    }
                    else
                    {
                        CurrentEnemy.SpriteOutLine(Color.red, 2);

                    }

                    CurrentPath = nodeBasePath;

                    CurrentTargetDestination = CurrentPath[0];

                    currentPos = CurrentPath.Count - 1;
                    FactoryCalls.GetAbility("Move").Process();

                }
                else
                {
                    CurrentEnemy = GridManager.Instance.enemy;
                    CurrentEnemy.SpriteOutLine(Color.red, 30);
                    CurrentEnemy.OnClick();

                   FactoryCalls.GetAbility("Attack").Process();

                }
            }
            else
            {
                if (CurrentTargetDestination != null)
                    CurrentTargetDestination.SpriteOutLine(Color.yellow, 0);

                CurrentPath = nodeBasePath;
                CurrentnodeBase.SpriteOutLine(Color.yellow, 0);
                CurrentTargetDestination = CurrentPath[0];
                currentPos = CurrentPath.Count - 1;
                FactoryCalls.GetAbility("Move").Process();


            }

        }


        public NodeBase CurrentNodeBase()
        {

            return CurrentnodeBase;

        }
        private void StopPath()
        {
            Move = false;
            CurrentnodeBase = CurrentPath[0];
            CurrentnodeBase.Obstacle(false);
            GridManager.Instance._playerNodeBase = CurrentnodeBase;
            if (CurrentEnemy!=null)
            {

                CurrentEnemy.OnClick();

            }
            CurrentnodeBase.SpriteOutLine(Color.yellow, 30);
            if (CurrentEnemy != null)
            {
                FactoryCalls.GetAbility("Attack").Process();
            }

        }


        private void Update()
        {
            if (Move)
            {
                transform.position = Vector3.MoveTowards(transform.position, CurrentPath[currentPos].transform.position, soldierUnit.speed * Time.deltaTime);
                if (transform.position == CurrentPath[currentPos].transform.position && currentPos != 0)
                {

                    CurrentnodeBase = CurrentPath[currentPos];

                    currentPos--;
                }
                else if (transform.position == CurrentPath[0].transform.position)
                {
                    StopPath();
                }
            }


        }
    }
}
