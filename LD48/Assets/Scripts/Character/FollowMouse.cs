using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool Follow = false;
    public bool ReadyToShoot = true;

    [SerializeField]
    private Color readyColor;

    [SerializeField]
    private Color notReadyColor;

    [SerializeField]
    Transform anchor;

    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
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

        if (ReadyToShoot)
        {
            rend.color = readyColor;
        }
        else
        {
            rend.color = notReadyColor;
        }
    }
}
