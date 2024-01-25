using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 0.7f;
    }

    void Update()
    {
        slider.value -= Time.deltaTime * 0.0005f;
        if (slider.value <= 0)
            fill.color = Color.black;
    }
}
