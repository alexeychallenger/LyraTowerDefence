using UnityEngine;

namespace LTD.Map
{
    public interface IMapHolder
    {
        float this[int x, int y] { get; set; }
        float this[Vector2Int v] { get; set; }

        int Width { get; }
        int Height { get; }

        bool IsWalkable(int x, int y);
        bool IsWalkable(Vector2Int v);
    }
}