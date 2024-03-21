using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject batteryUI;

    private Light lightCom;
    private bool isOn;

    public float rotSpeed;
    public float lightIntensity;
    public float posAdj;

    void Start()
    {
        lightCom = GetComponent<Light>();
        lightCom.intensity = 0;
    }

    void Update()
    {
        transform.position = playerCamera.transform.position - transform.forward * posAdj;
        transform.rotation = Quaternion.Lerp(transform.rotation, playerCamera.transform.rotation, Time.deltaTime * rotSpeed);
        if (batteryUI.GetComponent<BatteryManager>().slider.value != 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (isOn)
                {
                    StopCoroutine("LightOn");
                    lightCom.intensity = 0;
                    isOn = false;
                }
                else
                {
                    StartCoroutine("LightOn");
                    isOn = true;
                }
            }
        }
        else
        {
            StopCoroutine("LightOn");
            lightCom.intensity = 0;
            isOn = false;
        }

        if (isOn)
        {
            batteryUI.GetComponent<BatteryManager>().slider.value -= Time.deltaTime * 0.0005f;
        }
        else
            batteryUI.GetComponent<BatteryManager>().fill.color = Color.white;
    }

    IEnumerator LightOn()
    {
        for(int i = 0; i< 10; i++)
        {
            lightCom.intensity += lightIntensity / 10;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
