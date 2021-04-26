using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour
{
    [SerializeField]
    private GameObject heartOfKraken;

    [SerializeField]
    private HealthConfig healthConfig;

    [SerializeField]
    private GameObject dieEffect;

    [SerializeField]
    private GameObject graphics;

    [SerializeField]
    private ParticleSystem poisonEffect;

    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Hurtable>().Initialize(healthConfig);
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        var heart = Instantiate(heartOfKraken);
        heart.transform.position = transform.position;
        dieEffect.SetActive(true);
        graphics.SetActive(false);
        coll.enabled = false;
        poisonEffect.Stop();
        Invoke("ReallyKill", 3.0f);
    }

    public void ReallyKill()
    {
        Destroy(gameObject);
    }
}
