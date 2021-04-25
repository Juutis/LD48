using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOfKraken : MonoBehaviour
{
    [SerializeField]
    private HealthConfig healthConfig;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Hurtable>().Initialize(healthConfig);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
