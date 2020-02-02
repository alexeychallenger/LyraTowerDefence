using UnityEngine;

namespace LTD.PathFindingSystem
{
    public interface IMapHolder
    {
        float this[int x, int y] { get; }
        float this[Vector2Int v] { get; }

        int Width { get; }
        int Height { get; }

        bool IsWalkable(int x, int y);
        bool IsWalkable(Vector2Int v);
    }
}