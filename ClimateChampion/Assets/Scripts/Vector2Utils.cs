using UnityEngine;

namespace FineGameDesign.Utils
{
    public static class Vector2Utils
    {
        /// <summary>
        /// <a href="https://answers.unity.com/questions/161138/deriving-and-angle-from-two-points.html">From two points</a>
        /// </summary>
        public static float AngleBetweenPoints(Vector2 p1, Vector2 p2)
        {
            return Mathf.Atan2(p2.y-p1.y, p2.x-p1.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// <a href="https://answers.unity.com/questions/823090/equivalent-of-degree-to-vector2-in-unity.html">Equivalent of degree</a>
        /// </summary>
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        /// <summary>
        /// <a href="https://stackoverflow.com/questions/18851761/convert-an-angle-in-degrees-to-a-vector">Convert an angle</a>
        /// </summary>
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
    }
}
