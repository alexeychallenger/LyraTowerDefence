using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTD.Map;
using LTD.UI.WindowSystem;
using LTD.PathFindingSystem;

namespace LTD.Map.LevelDesing
{
    public class Level : MonoBehaviour
    {
        [SerializeField] public float cellSize = 1f;

        [SerializeField] public int xSize;
        [SerializeField] public int xOffset;
        [SerializeField] public int ySize;
        [SerializeField] public int yOffset;


        IMapHolder map;

        public void Init()
        {
            map = new Map(xSize, ySize, FindObjectsOfType<MapItem>());
            PathFinding.LoadMap(map);

            Debug.Log("Level is Loaded");

            MenuController.Instance.OpenWindow(WindowMenu.GameMenu);
        }

        public Vector2Int TransformToVector2Int(Vector3 pos)
        {
            var x = Mathf.FloorToInt(pos.x / cellSize) - xOffset;
            var y = Mathf.FloorToInt(pos.y / cellSize) - yOffset;

            return new Vector2Int(x, y);
        }
    }
}