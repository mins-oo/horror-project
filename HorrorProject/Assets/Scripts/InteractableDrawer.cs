using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrawer : Interactable
{
    GameObject uiPointer;

    public string text;
    bool isOpen = false;

    private void Start()
    {
        uiPointer = GameObject.Find("Pointer");
    }

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        if (!isOpen)
            StartCoroutine("DoorActive");
    }

    public override void OnLoseFocus()
    {

    }

    IEnumerator DoorActive()
    {
        isOpen = true;
        for(int i = 0; i < 50; i++)
        {
            transform.localPosition = transform.forward * i / 5000f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
