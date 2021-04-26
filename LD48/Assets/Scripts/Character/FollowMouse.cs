using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool Follow = false;

    [SerializeField]
    Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void LateUpdate()
    {
        if (Follow)
        {
            var mousePosition = Input.mousePosition;
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);
            transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        }
        else
        {
            transform.position = anchor.position;
        }
    }
}
