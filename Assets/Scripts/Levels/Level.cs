using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTD.Map;
using LTD.UI.WindowSystem;
using LTD.PathFindingSystem;
using LTD.PlayerControls;

namespace LTD.Map.LevelDesing
{
    public class Level : MonoBehaviour
    {
        public static Level Instance;

        [SerializeField] public float cellSize = 1f;

        [SerializeField] public int xSize;
        [SerializeField] public int xOffset;
        [SerializeField] public int ySize;
        [SerializeField] public int yOffset;


        IMapHolder map;

        public void Init()
        {
            Instance = this;

            map = new Map(xSize, ySize, FindObjectsOfType<MapItem>());
            PathFinding.LoadMap(map);

            Debug.Log("Level is Loaded");

            MenuController.Instance.OpenWindow(WindowMenu.GameMenu);

            PlayerControl.Instance.SetPause(false);
        }

        public void OnDestroy()
        {
            if (PlayerControl.Instance)
            {
                PlayerControl.Instance.Destroy();
            }

        }

        public Vector2Int TransformToVector2Int(Vector3 pos)
        {
            var x = Mathf.FloorToInt(pos.x / cellSize) - xOffset;
            var y = Mathf.FloorToInt(pos.y / cellSize) - yOffset;

            return new Vector2Int(x, y);
        }

        public void SelectTile(Vector3 selectPoint)
        {
            SelectTile(TransformToVector2Int(selectPoint));
        }

        public void SelectTile(Vector2Int selectedTile)
        {
            if (selectedTile.x < 0 || 
                selectedTile.y < 0 ||
                selectedTile.x >= xSize ||
                selectedTile.y >= ySize)
            {
                return;
            }

            Debug.Log($"Тык на {selectedTile.ToString()}");
        }
    }
}