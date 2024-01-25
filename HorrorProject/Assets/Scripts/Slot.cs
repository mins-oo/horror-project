using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;

    public void UpdateSlot()
    {
        itemIcon.sprite = item.image;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        itemIcon.gameObject.SetActive(false);
    }
}