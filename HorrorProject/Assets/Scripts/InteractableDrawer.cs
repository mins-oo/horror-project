using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrawer : Interactable
{
    GameObject uiPointer;
    UIControl uIControl;

    public string text;
    bool isOpen = false;

    private void Start()
    {
        uiPointer = GameObject.Find("Pointer");
        uIControl = uiPointer.GetComponent<UIControl>();
    }

    public override void OnFocus()
    {
        if (!isOpen)
            uIControl.ItemText(text);
    }

    public override void OnInteract()
    {
        if (!isOpen)
            StartCoroutine("DoorActive");
    }

    public override void OnLoseFocus()
    {
        uIControl.ItemText(" ");
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
