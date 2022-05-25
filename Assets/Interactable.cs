using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public bool isToggled = false;

    [SerializeField] private Animator anim; 

    // Update is called once per frame
    void Update()
    {
        if(anim != null){
            anim.SetBool("_state", isToggled);
        }
    }

    public void Toggle(){
        isToggled = !isToggled;
    }
}
