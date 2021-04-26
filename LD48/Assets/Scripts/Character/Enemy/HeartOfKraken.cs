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
        UIPopupManager.main.ShowPopup(
            "The Heart of Kraken",
            "It was the Heart of Kraken! That's what was causing the fish to attack the locals." +
            "You managed to destroy it with a well placed torpedo.\n\n" +
            "Thank for playing Fathomous, a game made for Ludum Dare 48!"
        );
        Destroy(gameObject);
    }
}
