using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrawerDesk : Interactable
{
    GameObject uiPointer;
    UIControl uIControl;

    int isOpen = 0;

    private void Start()
    {
        uiPointer = GameObject.Find("Pointer");
        uIControl = uiPointer.GetComponent<UIControl>();
    }

    public override void OnFocus()
    {
        if (isOpen == 0)
            uIControl.ItemText("open");
        else if (isOpen == 1)
            uIControl.ItemText("close");
    }

    public override void OnInteract()
    {
        if (isOpen == 0)
            StartCoroutine("DoorActive");
        else if(isOpen == 1)
            StartCoroutine("DoorInactive");
    }

    public override void OnLoseFocus()
    {
        uIControl.ItemText(" ");
    }

    IEnumerator DoorActive()
    {
        isOpen = 2;
        for (int i = 0; i < 50; i++)
        {
            transform.localPosition = transform.localPosition + transform.forward * i / 100000f;
            yield return new WaitForSeconds(0.01f);
        }
        isOpen = 1;
    }

    IEnumerator DoorInactive()
    {
        isOpen = 2;
        for (int i = 0; i < 50; i++)
        {
            transform.localPosition = transform.localPosition - transform.forward * i / 100000f;
            yield return new WaitForSeconds(0.01f);
        }
        isOpen = 0;
    }
}
