using UnityEngine;
using System.Collections;

public class CheckButtonDials : MonoBehaviour
{
    public GaugeScript coolantgauge;
    public GaugeScript pressuregauge;
    private float correctValueCoolant;
    private float correctValuePressure;
    private float lastCoolantValue;
    private float lastPressureValue;
    private bool coolantFixed;
    private bool pressureFixed;
    [Header("Turn on if you want the dials to always require a different value each time")]
    public bool alwaysChange;

    [Header("How many values can the coolant gauge select(put 1 higher than the amount of options(Ex: if there are 9 values put 10))")]
    private int minGaugeValueCoolant;
    [SerializeField] private int maxGaugeValueCoolant;

    [Header("How many values can the pressure gauge select(put 1 higher than the amount of options(Ex: if there are 9 values put 10))")]
    private int minGaugeValuePressure;
    [SerializeField] private int maxGaugeValuePressure;



    private void Start()
    {
        correctValueCoolant = 0f;
        correctValuePressure = 0f;
    }

    public void ChangeValue()
    {
        lastCoolantValue = correctValueCoolant;
        lastPressureValue = correctValuePressure;
        //random.range is inclusive for min number but exclusive for max number(meaning the min number is a possible output, but the max number is one number greater than the highest possible output)
        int CoolantValue = Random.Range(0, maxGaugeValueCoolant);
        //random.range is inclusive for min number but exclusive for max number(meaning the min number is a possible output, but the max number is one number greater than the highest possible output)
        int PressureValue = Random.Range(0, maxGaugeValuePressure);

        correctValueCoolant = coolantgauge.gaugeValues[CoolantValue];
        correctValuePressure = pressuregauge.gaugeValues[PressureValue];

        if (alwaysChange)
        {
            if (lastCoolantValue == correctValueCoolant)
            {
                //add coroutine to make keep randomizing value until it's different.
            }

        }
        coolantFixed = false;
        pressureFixed = false;

    }
}
