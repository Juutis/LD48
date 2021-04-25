using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private UnityEvent action;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push()
    {
        anim.SetBool("Trigger", true);
        action.Invoke();
    }
}
