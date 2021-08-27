using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{  
    public static float xPlayerBound = CameraEdgesBounds.screenBounds.x;
    public static float xEnemyBound =CameraEdgesBounds.screenBounds.x;
    public static float yEnemyBound = CameraEdgesBounds.screenBounds.y;
    public static float yPlayerBound = CameraEdgesBounds.screenBounds.y-0.5f;
}
