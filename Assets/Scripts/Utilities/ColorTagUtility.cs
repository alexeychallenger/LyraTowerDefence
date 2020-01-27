using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LTD.Utilities
{
    public static class ColorTagUtility
    {
        private static StringBuilder stringBuilder = new StringBuilder(3);

        public static string AddColorTag(Color color, string tag)
        {
            stringBuilder.Clear();
            stringBuilder.Append("<color=#");
            GetColorHexString(ref stringBuilder, color);
            stringBuilder.Append('>');
            stringBuilder.Append(tag);
            stringBuilder.Append("</color>");
            return stringBuilder.ToString();
        }

        public static string AddColorTag(this string message, Color color)
        {
            stringBuilder.Clear();
            stringBuilder.Append("<color=#");
            GetColorHexString(ref stringBuilder, color);
            stringBuilder.Append('>');
            stringBuilder.Append(message);
            stringBuilder.Append("</color>");
            return stringBuilder.ToString();
        }

        public static string AddColorTag(this object message, Color color)
        {
            stringBuilder.Clear();
            stringBuilder.Append("<color=#");
            GetColorHexString(ref stringBuilder, color);
            stringBuilder.Append('>');
            stringBuilder.Append(message);
            stringBuilder.Append("</color>");
            return stringBuilder.ToString();
        }

        private static void GetColorHexString(ref StringBuilder stringBuilder, Color color)
        {
            stringBuilder.Append(((int) (color.r * 255)).ToString("X02"));
            stringBuilder.Append(((int) (color.g * 255)).ToString("X02"));
            stringBuilder.Append(((int) (color.b * 255)).ToString("X02"));
        }
    }
}
