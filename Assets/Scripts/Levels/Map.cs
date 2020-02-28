using UnityEngine;
using LTD.Map.LevelDesing;

namespace LTD.Map
{
    public class Map : IMapHolder
    {
        public float this[Vector2Int v] => _map[v.x, v.y];

        public float this[int x, int y] => _map[x, y];

        public int _width;
        public int Width => _width;

        public int _height;
        public int Height => _height;

        public bool IsWalkable(int x, int y)
        {
            return _map[x, y] < 1f;
        }

        public bool IsWalkable(Vector2Int v)
        {
            return IsWalkable(v.x, v.y);
        }

        private float[,] _map;

        public Map(int xsize, int ysize, MapItem[] mapItems)
        {
            _width = xsize;
            _height = ysize;
            _map = new float[_width, _height];

            SetupMap(mapItems);
        }

        private void SetupMap(MapItem[] mapItems)
        {
            foreach(var mi in mapItems)
            {
                if(mi is Obstacle)
                {
                    _map[mi.position.x, mi.position.y] = ((Obstacle)mi).weight;
                }
            }
        }
    }
}
