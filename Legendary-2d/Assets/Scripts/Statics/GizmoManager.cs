using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoManager : MonoBehaviour
{
    public static void DrawLine(Vector2 from, Vector2 to)
    {
        Debug.DrawLine(from, to, Color.green);
    }
}
