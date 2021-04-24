using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool FindMainCameraAndFollowThat = true;

    void Start() {
        if (FindMainCameraAndFollowThat) {
            target = Camera.main.transform;
        }
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
