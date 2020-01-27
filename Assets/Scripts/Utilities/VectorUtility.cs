using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace LTD.Utilities
{
    public static class VectorUtility
    {
        public static bool AreEqual(this Vector2 expected, Vector3 actual, float error)
        {
            return FloatComparer.AreEqual(expected.x, actual.x, error)
                   && FloatComparer.AreEqual(expected.y, actual.y, error);
        }

        public static bool AreEqual(this Vector3 expected, Vector3 actual, float error)
        {
            return FloatComparer.AreEqual(expected.x, actual.x, error)
                   && FloatComparer.AreEqual(expected.y, actual.y, error)
                   && FloatComparer.AreEqual(expected.z, actual.z, error);
        }

        public static Vector2 ToVector2Xz(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector3 ToVector3Xz(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0f, vector2.y);
        }

        public static Vector2Int ToVector2IntXz(this Vector3Int vector3)
        {
            return new Vector2Int(vector3.x, vector3.z);
        }

        public static Vector3Int ToVector3IntXz(this Vector2Int vector2)
        {
            return new Vector3Int(vector2.x, 0, vector2.y);
        }
    }
}
