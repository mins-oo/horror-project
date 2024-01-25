using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable
{
    public GameObject uiControl;
    public Item item;

    public void SetItem(Item _item)
    {
        item.name = _item.name;
        item.image = _item.image;
    }

    public override void OnFocus()
    {
        uiControl.GetComponent<UIControl>().ItemText(item.name);
    }

    public override void OnInteract()
    {
        if(uiControl.GetComponent<UIControl>().AddItem(item, gameObject))
        {
            uiControl.GetComponent<UIControl>().Init();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public override void OnLoseFocus()
    {
        uiControl.GetComponent<UIControl>().ItemText(" ");
    }
}