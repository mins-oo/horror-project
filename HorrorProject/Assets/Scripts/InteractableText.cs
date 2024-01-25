using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableText : Interactable
{
    public GameObject uiPointer;
    public GameObject player;

    UIControl uIControl;
    PlayerControl playerControl;

    public Image image;
    bool isOpen = false;

    private void Start()
    {
        uIControl = uiPointer.GetComponent<UIControl>();
        playerControl = player.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isOpen = false;
            image.gameObject.SetActive(false);
            playerControl.enabled = true;
        }
    }

    public override void OnFocus()
    {
        if (!isOpen)
            uIControl.ItemText("read");
    }

    public override void OnInteract()
    {
        if (!isOpen)
        {
            isOpen = true;
            image.gameObject.SetActive(true);
            playerControl.enabled = false;
        }
    }

    public override void OnLoseFocus()
    {
        uIControl.ItemText(" ");
    }
}
