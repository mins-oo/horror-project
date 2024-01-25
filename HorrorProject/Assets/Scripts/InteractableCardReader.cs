using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCardReader : Interactable
{
    public GameObject assinedDoor;
    public GameObject uiPointer;

    UIControl uIControl;

    public Material[] mat = new Material[3];
    public string roomName;
    public string assignedCardName;

    bool isOpen = false;
    float timer;

    private void Start()
    {
        uIControl = uiPointer.GetComponent<UIControl>();
    }

    private void Update()
    {
        if (timer < 0)
        {
            Material[] myMaterial = gameObject.GetComponent<MeshRenderer>().materials;
            myMaterial[1] = mat[2];
            gameObject.GetComponent<MeshRenderer>().materials = myMaterial;
        }
        else
            timer -= Time.deltaTime;
    }

    public override void OnFocus()
    {
        uIControl.ItemText(roomName);
    }

    public override void OnInteract()
    {
        timer = 2f;
        Material[] myMaterial = gameObject.GetComponent<MeshRenderer>().materials;

        if (uIControl.items[uIControl.slot] != null)
        {
            if(uIControl.items[uIControl.slot].GetComponent<InteractableObject>().item.name == assignedCardName)
            {
                myMaterial[1] = mat[1];
                StartCoroutine("DoorActive");
            }
            else
                myMaterial[1] = mat[0];

        }
        else
        {
            myMaterial[1] = mat[0];
        }
        gameObject.GetComponent<MeshRenderer>().materials = myMaterial;
    }

    public override void OnLoseFocus()
    {
        uIControl.ItemText(" ");
    }

    IEnumerator DoorActive()
    {
        Quaternion pos = assinedDoor.transform.localRotation;

        if (!isOpen)
        {
            isOpen = true;
            for (int i = 0; i <= 20; i++)
            {
                assinedDoor.transform.localRotation = Quaternion.Euler(pos.x, pos.y - i * 5, pos.z);
                yield return (0.01f);
            }
        }
        else
        {
            isOpen = false;
            for (int i = 20; i >= 0; i--)
            {
                assinedDoor.transform.localRotation = Quaternion.Euler(pos.x, pos.y - i * 5, pos.z);
                yield return (0.01f);
            }
        }
    }
}
