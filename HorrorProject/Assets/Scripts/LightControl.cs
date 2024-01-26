using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public GameObject flashLight;
    public GameObject batteryUI;
    public GameObject worldLight;

    public Light lightCom;
    bool isOn;

    public float rotSpeed;
    public float lightIntensity;

    void Start()
    {
        lightCom = flashLight.GetComponentInChildren<Light>();
        lightCom.intensity = 0;
    }

    void Update()
    {
        flashLight.transform.position = transform.position;
        flashLight.transform.rotation = Quaternion.Lerp(flashLight.transform.rotation, transform.rotation, Time.deltaTime * rotSpeed);
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

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Monster" && isOn)
        {
            if(collider.GetComponentInParent<MonsterLocomotion>().agent.speed == 6)
            {
                batteryUI.GetComponent<BatteryManager>().slider.value -= Time.deltaTime * 0.05f;
                collider.GetComponentInParent<MonsterLocomotion>().Stun();
                StartCoroutine("Blink");
                batteryUI.GetComponent<BatteryManager>().fill.color = Color.red;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Monster")
        {
            batteryUI.GetComponent<BatteryManager>().fill.color = Color.white;
        }
    }

    IEnumerator LightOn()
    {
        for(int i = 0; i< 10; i++)
        {
            lightCom.intensity += lightIntensity / 10;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Blink()
    {
        lightCom.intensity = 0;
        yield return new WaitForSeconds(0.1f);
        lightCom.intensity = lightIntensity;
        yield return new WaitForSeconds(0.1f);
        if (!isOn)
           lightCom.intensity = 0;
    }
}
