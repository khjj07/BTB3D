using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace GizmosExtensionMethods
{
    public static class GizmosExtension
    {
        public static void DrawArrow(Vector3 from, Vector3 to, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 30.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);

            Vector3 right = Quaternion.LookRotation(to-from) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(to - from) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(to, right * arrowHeadLength);
            Gizmos.DrawRay(to, left * arrowHeadLength);
        }
    }
}
