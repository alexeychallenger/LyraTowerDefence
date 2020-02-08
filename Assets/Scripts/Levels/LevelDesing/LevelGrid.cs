using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LTD.Map.LevelDesing
{
    public class LevelGrid : MonoBehaviour
    {
        [SerializeField] private bool _showGrid;
        [SerializeField] private bool _showCoords;
        [SerializeField] private Color _gridColor;

        private void OnDrawGizmos()
        {
            var level = FindObjectOfType<Level>();

            DrawGrid(level);
            DrawCoord(level);
        }

        private void DrawGrid(Level level)
        {
            if (!_showGrid)
                return;

            var cellSize = level.cellSize;
            var xSize = level.xSize;
            var ySize = level.ySize;
            var xOffset = level.xOffset;
            var yOffset = level.yOffset;

            for (int ix = 0; ix < xSize + 1; ix++)
            {
                int x = Mathf.FloorToInt((ix + xOffset) * cellSize);
                int yOff = Mathf.FloorToInt(yOffset * cellSize);
                int y = Mathf.FloorToInt((ySize + yOffset) * cellSize);
                Debug.DrawLine(new Vector3(x, yOff, 0), new Vector3(x, y, 0), _gridColor);
            }

            for (int iy = 0; iy < ySize + 1; iy++)
            {
                int y = Mathf.FloorToInt((iy + yOffset) * cellSize);
                int xOff = Mathf.FloorToInt(xOffset * cellSize);
                int x = Mathf.FloorToInt((xSize + xOffset) * cellSize);

                Debug.DrawLine(new Vector3(xOff, y, 0), new Vector3(x, y, 0), _gridColor);
            }
        }
        
        private void DrawCoord(Level level)
        {
            if (!_showCoords)
                return;

            var cellSize = level.cellSize;
            var xSize = level.xSize;
            var ySize = level.ySize;
            var xOffset = level.xOffset;
            var yOffset = level.yOffset;

            var obstacles = FindObjectsOfType<Obstacle>();
            var list = new List<Vector2Int>();
            foreach (var obs in obstacles)
            {
                list.Add(level.TransformToVector2Int(obs.transform.position));
            }

            for (int ix = 0; ix < xSize; ix++)
            {
                float x = Mathf.FloorToInt((ix + xOffset) * cellSize) + 0.5f * cellSize;
                for (int iy = 0; iy < ySize; iy++)
                {
                    float y = Mathf.FloorToInt((iy + yOffset) * cellSize) + 0.5f * cellSize;
                    string label = $"[{ix.ToString()} {iy.ToString()}]";
                    var gui = new GUIStyle();
                    gui.alignment = TextAnchor.MiddleCenter;
                    gui.normal.textColor = _gridColor;

                    int obsIndex = list.IndexOf(new Vector2Int(ix, iy));
                    if (obsIndex >= 0)
                    {
                        var value = obstacles[obsIndex].weight;
                        if (value > 0)
                        {
                            gui.normal.textColor = GetColor(value);
                        }
                    }

                    Handles.Label(new Vector3(x, y, 0), label, gui);
                }
            }
        }

        private Color GetColor(float weight)
        {
            if (weight < 0.1f)
            {
                return Color.white;
            }
            else if (weight < 0.4f)
            {
                return Color.green;
            }
            else if (weight < 0.9f)
            {
                return Color.yellow;
            }
            else if (weight == 1f)
            {
                return Color.black;
            }
            return Color.red;
        }
    }
}