using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Image crosshair;

    private Vector3 defaultCrossScale;

    UnityEvent onInteract;


    // Start is called before the first frame update
    void Start()
    {
        defaultCrossScale = crosshair.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f, interactableLayer))
        {
            Debug.Log(hit.collider.name);
            if(hit.collider.GetComponent<Interactable>() != false)
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                crosshair.transform.localScale = defaultCrossScale * 2;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    onInteract.Invoke();
                }
            }
        } 
        else
        {
                crosshair.transform.localScale = defaultCrossScale;
        }
    }
}
