using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Submarine submarine;
    Renderer rend;

    private float accelerationSpeed = 10.0f;
    private float rotationSpeed = 180.0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        submarine = GetComponent<Submarine>();
        submarine.Init(rend.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        submarine.Accelerate(Input.GetAxis("Vertical") * accelerationSpeed * Time.deltaTime);
        submarine.Rotate(Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
    }
}