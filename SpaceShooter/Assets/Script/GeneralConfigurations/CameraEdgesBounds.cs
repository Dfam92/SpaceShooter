using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgesBounds : MonoBehaviour
{
    public static Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

   
}
