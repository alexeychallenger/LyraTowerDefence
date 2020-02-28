using UnityEngine;

namespace LTD.Map.LevelDesing
{
    public class Obstacle : MapItem
    {
        [SerializeField, Range(0f, 1f)] public float weight;

        private void Awake()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
