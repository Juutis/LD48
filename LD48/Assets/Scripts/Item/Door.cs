using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private bool firstSubmarineHit = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        anim.SetBool("Trigger", true);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (firstSubmarineHit && other.gameObject.tag == "Player") {
            firstSubmarineHit = false;
            UIPopupManager.main.ShowPopup("Clearly a pathway", "The must be some sort of way to open this!");
        }
    }
}
