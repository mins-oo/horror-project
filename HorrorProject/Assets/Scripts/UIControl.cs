using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public int slot;
    float timer;

    public GameObject inventory;
    public Camera player;
    public TextMeshProUGUI textUI;

    public Slot[] slots;
    public List<GameObject> items = new List<GameObject>();

    void Update()
    {
        transform.position = new Vector3(slot * 90, inventory.transform.position.y, 0);

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            timer = 5f;
            inventory.transform.position = new Vector3(inventory.transform.position.x, 0, 0);
            if (slot < 4)
                slot++;
            else
                slot = 0;

            Init();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            timer = 5f;
            inventory.transform.position = new Vector3(inventory.transform.position.x, 0, 0);
            if (slot > 0)
                slot--;
            else
                slot = 4;

            Init();
        }
        else
        {
            if(inventory.transform.position == Vector3.zero)
            {
                timer = timer - Time.deltaTime;
                if(timer < 0)
                    StartCoroutine("InventoryOff");
            }
        }

        //������ slot�� ������Ʈ�� �ִٸ� �տ� ���
        if (items[slot] != null)
        {
            items[slot].SetActive(true);
            items[slot].transform.position = Vector3.Lerp(items[slot].transform.position, player.transform.position + (player.transform.forward + player.transform.right * 0.8f - player.transform.up * 0.5f) * 0.8f, Time.deltaTime * 30);
            items[slot].transform.rotation = player.transform.rotation;
            items[slot].layer = 0;
        }
    }

    public void Init()
    {
        for (int i = 0; i < 5; i++)
        {
            if (items[i] != null)
                items[i].SetActive(false);
        }

        if (items[slot] != null)
        {
            items[slot].transform.position = player.transform.position + (player.transform.forward + player.transform.right * 0.8f - player.transform.up * 0.5f) * 0.8f;
        }
    }

    IEnumerator InventoryOff()
    {
        for (int i = 0; i < 10; i++)
        {
            inventory.transform.position = new Vector3(inventory.transform.position.x, -11 * i, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public bool AddItem(Item _item, GameObject _gameObject)
    {
        timer = 5f;
        inventory.transform.position = new Vector3(inventory.transform.position.x, 0, 0);

        //pointing slot�� ��������� pointing slot�� �߰�
        if (slots[slot].item.image == null)
        {
            return AddSlot(slot, _item, _gameObject);
        }

        //pointing slot�� �������� ������������ �� slot Ž��
        for (int i = 0; i < 5; i++)
        {
            //�� slot�� �߰�
            if (slots[i].item.image == null)
            {
                return AddSlot(i, _item, _gameObject);
            }
        }

        //�� slot�� ������ ������Ʈ�� player ��ġ�� ��ȯ�ϰ� pointing slot�� �߰�
        items[slot].GetComponent<BoxCollider>().enabled = true;
        items[slot].transform.position = player.transform.position + player.transform.forward;
        items[slot].GetComponent<Rigidbody>().isKinematic = false;
        items[slot].GetComponent<Rigidbody>().velocity = Vector3.zero;
        items[slot].layer = 6;

        return AddSlot(slot, _item, _gameObject);
    }

    bool AddSlot(int n, Item _item, GameObject _gameObject)
    {
        items[n] = _gameObject;
        slots[n].item = _item;
        slots[n].UpdateSlot();
        return true;
    }

    public void ItemText(string itemName)
    {
        if(itemName != null)
            textUI.text = itemName;
    }

    public void uploadItem()
    {
        items[slot].transform.position = player.transform.position + player.transform.forward;
        items[slot].GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}