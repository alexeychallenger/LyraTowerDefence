using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTD.PathFindingSystem
{
    internal class PointList
    {
        private readonly List<Point> pointsList = new List<Point>();

        public int Count => pointsList.Count;
        public List<Point> GetList => pointsList;

        public void AddElement(Point point)
        {
            if (HasElement(point))
            {
                return;
            }

            pointsList.Add(point);
        }

        public void AddElements(List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                AddElement(points[i]);
            }
        }

        public void UpdateElement(Point point)
        {
            int i = FindElement(point);
            if (i >= 0)
            {
                pointsList[i] = point;
            }
        }

        public bool HasElement(Point point)
        {
            int i = FindElement(point);
            return i >= 0;
        }

        public void RemoveElement(Point point)
        {
            int i = FindElement(point);
            if (i >= 0)
            {
                pointsList.RemoveAt(i);
            }
        }

        public Point GetElement(Point point)
        {
            int i = FindElement(point);
            return pointsList[i];
        }

        private int FindElement(Point point)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                if (pointsList[i].IsEqual(point))
                {
                    return i;
                }
            }
            return -1;
        }

        public Point GetElementWithMinF()
        {
            float minF = float.PositiveInfinity;
            int minEl = 0;

            for (int i = 0; i < pointsList.Count; i++)
            {
                if (pointsList[i].f >= minF) continue;
                
                minF = pointsList[i].f;
                minEl = i;
            }

            return pointsList[minEl];
        }
    }
}
