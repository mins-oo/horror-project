using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable
{
    public GameObject uiControl;
    public Item item;

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    public override void OnLoseFocus()
    {

    }
}