using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHordes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnDestroy();
    }
    private void OnDestroy()
    {
        if (transform.parent != null) // if object has a parent
        {
            if (transform.childCount <= 1) // if this object is the last child
            {
                Destroy(transform.parent.gameObject, 0.1f); // destroy parent a few frames later
            }
        }
    }
}
