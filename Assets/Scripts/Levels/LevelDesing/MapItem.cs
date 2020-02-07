using UnityEngine;
using LTD.Map.LevelDesing;

public class MapItem : MonoBehaviour
{
    [SerializeField] public Vector2Int position;

    private void OnDrawGizmos()
    {
        var level = FindObjectOfType<Level>();
        position = level.TransformToVector2Int(transform.position);
    }
}
