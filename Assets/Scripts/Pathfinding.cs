using UnityEngine;
using System.Collections.Generic;

internal class Point
{
    public int x;
    public int y;
    public float g;
    public float h;
    public float f;
    public Point parent;

    public Point(int xs = 0, int ys = 0, float gs = -1, float hs = -1, float fs = -1)
    {
        x = xs;
        y = ys;
        g = Mathf.Max(gs, 0);
        h = Mathf.Max(hs, 0);
        f = Mathf.Max(fs, 0);
        parent = null;
    }

    public Point(Vector2Int vector)
    {
        x = vector.x;
        y = vector.y;
        g = 0;
        h = 0;
        f = 0;
        parent = null;
    }

    public Vector2Int GetNewVector()
    {
        return new Vector2Int(x, y);
    }

    public override string ToString()
    {
        return string.Format("[{0}, {1}]", x.ToString(), y.ToString());
    }

    public bool Equals(object obj)
    {
        Point hex = (Point)obj;
        return hex.x == x
            && hex.y == y;
    }
}


public static class Pathfinding
{
    private const float CELL_SIZE = 1;
    private const float MIN_G = 1f;

    private static int width;
    private static int height;

    private static IMapHolder map;

    private class PointList
    {
        private List<Point> array;
        private List<float> fValues;

        public int Count
        {
            get
            {
                return array.Count;
            }
        }

        public PointList()
        {
            Clear();
        }

        public void Clear()
        {
            array = new List<Point>();
            fValues = new List<float>();
        }

        public void AddElement(Point el)
        {
            if (HasElement(el))
            {
                return;
            }

            array.Add(el);
            fValues.Add(el.f);
        }

        public void AddElement(List<Point> els)
        {
            for (int i = 0; i < els.Count; i++)
            {
                AddElement(els[i]);
            }
        }

        public void UpdateElement(Point el)
        {
            int i = FindElement(el);
            if (i >= 0)
            {
                array[i] = el;
            }        }

        public bool HasElement(Point el)
        {
            int i = FindElement(el);
            return i >= 0;
        }

        public void RemoveElement(Point el)
        {
            int i = FindElement(el);
            if (i >= 0)
            {
                array.RemoveAt(i);
                fValues.RemoveAt(i);
            }
        }

        public Point GetElement(Point el)
        {
            int i = FindElement(el);
            return array[i];
        }

        private int FindElement(Point p)
        {
            for(int i = 0; i < array.Count; i++)
            {
                if (array[i].x == p.x && array[i].y == p.y)
                {
                    return i;
                }
            }
            return -1;
        }

        public Point GetElementWithMinF()
        {
            float minf = float.PositiveInfinity;
            int minEl = 0;
            for (int i = 0; i < fValues.Count; i++)
            {
                if (minf > fValues[i])
                {
                    minf = fValues[i];
                    minEl = i;
                }
            }

            return array[minEl];
        }

        public List<Point> GetList()
        {
            return array;
        }
    }

    public static void LoadMap(IMapHolder mapHolder)
    {
        map = mapHolder;
        width = mapHolder.Width;
        height = mapHolder.Height;
    }

    public static List<Vector2Int> FindPath(Vector2Int _source, Vector2Int _target)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var target = new Point(_target);
        var source = new Point(_source);

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

        lst.Add(step.GetNewVector());
        while (step.parent != null)
        {
            step = step.parent;
            lst.Insert(0, step.GetNewVector());
        }
        lst.RemoveAt(0);

        return lst;
    }
}
