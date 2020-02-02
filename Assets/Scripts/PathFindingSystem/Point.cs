using UnityEngine;

namespace LTD.PathFindingSystem
{
    internal class Point
    {
        public int x;
        public int y;
        public float g;
        public float h;
        public float f;
        public Point parent;

        public Vector2Int ToVector2Int => new Vector2Int(x, y);

        public Point(int x = 0, int y = 0, float g = -1, float h = -1, float f = -1)
        {
            this.x = x;
            this.y = y;
            this.g = Mathf.Max(g, 0);
            this.h = Mathf.Max(h, 0);
            this.f = Mathf.Max(f, 0);
            parent = null;
        }

        public Point(ref Vector2Int vector)
        {
            x = vector.x;
            y = vector.y;
            g = 0;
            h = 0;
            f = 0;
            parent = null;
        }

        public override string ToString()
        {
            return $"[x: {x.ToString()}, y: {y.ToString()}]";
        }

        public bool IsEqual(Point point)
        {
            return point.x == x
                   && point.y == y;
        }
    }
}