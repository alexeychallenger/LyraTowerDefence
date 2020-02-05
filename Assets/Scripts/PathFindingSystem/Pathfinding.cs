using System.Collections.Generic;
using UnityEngine;

namespace LTD.PathFindingSystem
{
    public static class PathFinding
    {
        private const float CELL_SIZE = 1;
        private const float MIN_G = 1f;

        private static int width;
        private static int height;

        private static IMapHolder map;

        public static void LoadMap(IMapHolder mapHolder)
        {
            map = mapHolder;
            width = mapHolder.Width;
            height = mapHolder.Height;
        }

        public static List<Vector2Int> FindPath(Vector2Int _source, Vector2Int _target)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            var target = new Point(ref _target);
            var source = new Point(ref _source);

            if (source.x < 0 || source.x >= width || source.y < 0 || source.y >= height
                || target.x < 0 || target.x >= width || target.y < 0 || target.y >= height)
            {
                result.Add(_target);
                return result;
            }

            result = CalcPath(source, target);
            return result;
        }

        private static Vector2 GetCellCenter(int xs, int ys)
        {
            return (new Vector2(xs + 0.5f, ys + 0.5f) * CELL_SIZE);
        }

        private static float Distance(Point p1, Point p2)
        {
            Vector2 p1_center = GetCellCenter(p1.x, p1.y);
            Vector2 p2_center = GetCellCenter(p2.x, p2.y);
            float dist = Mathf.Sqrt((p1_center.x - p2_center.x) * (p1_center.x - p2_center.x)
                                    + (p1_center.y - p2_center.y) * (p1_center.y - p2_center.y));
            return dist;
        }

        private static List<Point> GetNeighbors(int x, int y, bool all = false)
        {
            List<Point> neighbors = new List<Point>();

            //int[,] deltas = new int[8, 2]
            //{
            //    { 1, -1}, { 1, 0}, { 1, 1},
            //    { 0, -1},          { 0, 1},
            //    {-1, -1}, {-1, 0}, {-1, 1}
            //};
            int[,] deltas = new int[4, 2]
            {
                { 1, 0},
                { 0, -1},          { 0, 1},
                {-1, 0}
            };
            for (int i = 0; i < deltas.GetLength(0); i++)
            {
                int xs = x + deltas[i, 0];
                int ys = y + deltas[i, 1];
                if (xs < 0 || ys < 0 || xs >= width || ys >= height)
                {
                    if (all) neighbors.Add(new Point(-1, -1));

                    continue;
                }

                neighbors.Add(new Point(xs, ys));
            }

            return neighbors;
        }

        private static List<Point> GetNeighbors(Point hp)
        {
            return GetNeighbors(hp.x, hp.y);
        }

        private static List<Point> GetNeighbors(int x, int y)
        {
            return GetNeighbors(x, y, false);
        }

        private static List<Vector2Int> CalcPath(Point source, Point target)
        {
            var openList = new PointList();
            var closeList = new PointList();

            source.g = 0;
            source.h = Distance(source, target);
            source.f = source.g + source.h;
            source.parent = new Point(-1, -1);
            openList.AddElement(source);

            Point step;
            while (openList.Count > 0)
            {
                step = openList.GetElementWithMinF();
                List<Point> neighbors = GetNeighbors(step.x, step.y);
                openList.RemoveElement(step);
                closeList.AddElement(step);

                for (int i = 0; i < neighbors.Count; i++)
                {
                    Point cell = neighbors[i];
                    if (!map.IsWalkable(cell.x, cell.y))
                        continue;

                    if (closeList.HasElement(cell))
                        continue;

                    float weight = map[cell.x, cell.y];

                    if (openList.HasElement(cell))
                    {
                        Point st = openList.GetElement(cell);
                        if (st.g > step.g + MIN_G + weight)
                        {
                            st.g = step.g + MIN_G + weight;
                            st.h = Distance(st, target);
                            st.f = st.g + st.h;
                            st.parent = step;
                            openList.UpdateElement(st);
                        }
                    }
                    else
                    {
                        cell.g = step.g + MIN_G + weight;
                        cell.h = Distance(cell, target);
                        cell.f = cell.g + cell.h;
                        cell.parent = step;
                        openList.AddElement(cell);
                    }
                }
                if (target.x == step.x && target.y == step.y)
                {
                    break;
                }
            }

            List<Vector2Int> lst = new List<Vector2Int>();
            if (openList.Count == 0)
                return lst;

            step = closeList.GetElement(target);

            lst.Add(step.ToVector2Int);
            while (step.parent != null)
            {
                step = step.parent;
                lst.Insert(0, step.ToVector2Int);
            }
            lst.RemoveAt(0);

            return lst;
        }
    }
}