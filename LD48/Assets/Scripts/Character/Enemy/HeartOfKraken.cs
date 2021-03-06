using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOfKraken : MonoBehaviour
{
    [SerializeField]
    private HealthConfig healthConfig;

    [SerializeField]
    private ParticleSystem poison;

    [SerializeField]
    private GameObject dieEffect;

    [SerializeField]
    private GameObject gfx;

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
        dieEffect.SetActive(true);
        gfx.SetActive(false);
        poison.Stop();
        coll.enabled = false;
        Invoke("ReallyKill", 10.0f);
    }

    public void ReallyKill()
    {
        UIPopupManager.main.ShowPopup(
            "The Heart of Kraken",
            "It was the Heart of Kraken! That's what was causing the fish to attack the locals." +
            "You managed to destroy it with a well placed torpedo.\n\n" +
            "Thank for playing Sunk Cost, a game made for Ludum Dare 48!"
        );
        Destroy(gameObject);
    }
}
