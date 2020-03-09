using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTD.Map;
using LTD.UI.WindowSystem;
using LTD.PathFindingSystem;
using LTD.PlayerControls;
using LTD.Lua;

namespace LTD.Map.LevelDesing
{
    public class Level : MonoBehaviour
    {
        public static Level Instance;

        [SerializeField] public string levelName;

        [Header("Grid settings")]
        [SerializeField] public float cellSize = 1f;
        [SerializeField] public int xSize;
        [SerializeField] public int xOffset;
        [SerializeField] public int ySize;
        [SerializeField] public int yOffset;

        private GameObject buildingContainer;
        private LuaResponder lua;
        private IMapHolder map;

        public void Init()
        {
            Instance = this;

            map = new Map(xSize, ySize, FindObjectsOfType<MapItem>());
            PathFinding.LoadMap(map);

            Debug.Log($"Level [{levelName}] is loaded");

            MenuController.Instance.OpenWindow(WindowMenu.GameMenu);

            PlayerControl.Instance.SetPause(false);

            buildingContainer = new GameObject("Buildings");
            buildingContainer.transform.SetParent(transform.parent);

            lua = new LuaResponder();
            lua.LoadScript(levelName);
            lua.CallCoroutine("init");
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

        public Vector3 TransformToVector3(Vector2Int pos)
        {
            var x = (pos.x + xOffset) * cellSize;
            var y = (pos.y + yOffset) * cellSize;

            return new Vector3(x, y, 0);
        }

        public Vector2Int? SelectTile(Vector3 selectPoint)
        {
            var selectedTile = TransformToVector2Int(selectPoint);

            if (selectedTile.x < 0 ||
                selectedTile.y < 0 ||
                selectedTile.x >= xSize ||
                selectedTile.y >= ySize)
            {
                return null;
            }

            Debug.Log($"Тык на {selectPoint.ToString()} | {selectedTile.ToString()}");
            return selectedTile;
        }

        public void PlaceBuilding(GameObject obj, Vector2Int tile)
        {
            Debug.Log($"Билжу {obj.name} на {tile.ToString()}");
            var building = Instantiate(obj, buildingContainer.transform);
            building.transform.position = TransformToVector3(tile) + new Vector3(cellSize / 2f, cellSize / 2f, 0);

            map[tile] = 1f;
        }
    }
}