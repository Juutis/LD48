using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splasher : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;

    private float prevY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        prevY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0.0f && prevY <= 0.0f)
        {
            var splash = Instantiate(effect);
            splash.transform.position = new Vector2(transform.position.x, 0.0f);
        }
        else if (transform.position.y < 0.0f && prevY >= 0.0f)
        {
            var splash = Instantiate(effect);
            splash.transform.position = new Vector2(transform.position.x, 0.0f);
        }

        prevY = transform.position.y;
    }
}
