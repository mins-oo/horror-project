using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCardReader : Interactable
{
    public GameObject assinedDoor;
    public GameObject uiPointer;

    public Material[] mat = new Material[3];
    public string roomName;
    public string assignedCardName;

    bool isOpen = false;
    float timer;

    private void Start()
    {

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

    }

    public override void OnInteract()
    {
        timer = 2f;
        Material[] myMaterial = gameObject.GetComponent<MeshRenderer>().materials;
       
        myMaterial[1] = mat[1];
        StartCoroutine("DoorActive");

        //myMaterial[1] = mat[0]; disable

        gameObject.GetComponent<MeshRenderer>().materials = myMaterial;
    }

    public override void OnLoseFocus()
    {

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
