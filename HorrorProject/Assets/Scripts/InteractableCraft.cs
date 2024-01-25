using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCraft : Interactable
{
    public GameObject uiPointer;
    public string craftItem;

    UIControl uIControl;

    private void Start()
    {
        uIControl = uiPointer.GetComponent<UIControl>();
    }

    public override void OnFocus()
    {
        uIControl.ItemText("craft");
    }

    public override void OnInteract()
    {
        if (uIControl.items[uIControl.slot].GetComponent<InteractableObject>().item.name == craftItem)
        {
            uIControl.uploadItem();
        }
    }

    public override void OnLoseFocus()
    {
        uIControl.ItemText(" ");
    }
}
