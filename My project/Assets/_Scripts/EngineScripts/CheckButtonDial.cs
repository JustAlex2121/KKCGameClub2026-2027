using UnityEngine;
using System.Collections;

public class CheckButtonDial : MonoBehaviour
{
    public GaugeScript coolantgauge;
    public GaugeScript pressuregauge;
    private float correctValueCoolant;
    private float correctValuePressure;
    private float lastCoolantValue;
    private float lastPressureValue;
    private bool coolantFixed;
    private bool pressureFixed;

    //lights that will tell the player if they have the lever in the right position
    public GameObject lightcoolant;
    public GameObject lightpressure;

    // Reference to the main engine script
    private EngineControlScript enginecontrol;

    [Header("Turn on if you want the dials to always require a different value each time")]
    public bool alwaysChange;


    private int minGaugeValueCoolant;
    private int minGaugeValuePressure;


    [Header("How many values can the pressure gauge select(put 1 higher than the amount of options(Ex: if there are 9 values put 10))")]
    [SerializeField] private int maxGaugeValueCoolant;
    [SerializeField] private int maxGaugeValuePressure;



    private void Start()
    {
        enginecontrol = Object.FindFirstObjectByType<EngineControlScript>();
        correctValueCoolant = 0f;
        correctValuePressure = 0f;
        coolantFixed = false;
        pressureFixed = false;
        ChangeValue();
    }

    private void OnMouseDown()
    {
        if (enginecontrol.engineoverheated && !enginecontrol.enginedialsokay)
        {
            CheckEngine();
        }   
    }


    private void CheckEngine()
    {
        if (coolantgauge.selectedValue == correctValueCoolant)
        {
            if (!coolantFixed)
            {
                lightcoolant.GetComponent<MeshRenderer>().material = enginecontrol.correct;
                coolantFixed = true;
            }
        }
        else
        {
            if (coolantFixed)
            {
                lightcoolant.GetComponent<MeshRenderer>().material = enginecontrol.wrong;
                coolantFixed = false;
            }
        }
        if (pressuregauge.selectedValue == correctValuePressure)
        {
            if (!pressureFixed)
            {
                lightpressure.GetComponent<MeshRenderer>().material = enginecontrol.correct;
                pressureFixed = true;
            }
        }
        else
        {
            if (pressureFixed)
            {
                lightpressure.GetComponent<MeshRenderer>().material = enginecontrol.wrong;
                pressureFixed = false;
            }
        }

        if (coolantFixed && pressureFixed)
        {
            enginecontrol.enginedialsokay = true;
            enginecontrol.CheckEngineStatus();
            enginecontrol.coolantdialcollider.enabled = false;
            enginecontrol.pressuredialcollider.enabled = false;
            Debug.Log("dials okay");
        }
    }

    public void ChangeValue()
    {
        coolantFixed = false;
        pressureFixed = false;
        lastCoolantValue = correctValueCoolant;
        lastPressureValue = correctValuePressure;
        //random.range is inclusive for min number but exclusive for max number(meaning the min number is a possible output, but the max number is one number greater than the highest possible output)
        int CoolantValue = Random.Range(0, maxGaugeValueCoolant);
        //random.range is inclusive for min number but exclusive for max number(meaning the min number is a possible output, but the max number is one number greater than the highest possible output)
        int PressureValue = Random.Range(0, maxGaugeValuePressure);

        correctValueCoolant = coolantgauge.gaugeValues[CoolantValue];
        correctValuePressure = pressuregauge.gaugeValues[PressureValue];
        Debug.Log("the correct coolant value is " + correctValueCoolant + "and the correct pressure value is " + correctValuePressure);

        if (alwaysChange)
        {
            if (lastCoolantValue == correctValueCoolant)
            {
                StartCoroutine(RandomizeCoolantValue());
            }
            if(lastPressureValue == correctValuePressure)
            {
                StartCoroutine(RandomizePressureValue());
            }

        }
        coolantFixed = false;
        pressureFixed = false;



    }

    private IEnumerator RandomizeCoolantValue()
    {
        int CoolantValue;
        while(lastCoolantValue == correctValueCoolant)
        {
            CoolantValue = Random.Range(0, maxGaugeValueCoolant);

            correctValueCoolant = coolantgauge.gaugeValues[CoolantValue];
        }
        Debug.Log("the rerolled correct coolant value is " + correctValueCoolant);
        yield break;
        
    }

    private IEnumerator RandomizePressureValue()
    {
        int PressureValue;
        while (lastPressureValue == correctValuePressure)
        {
            PressureValue = Random.Range(0, maxGaugeValuePressure);

            correctValuePressure = pressuregauge.gaugeValues[PressureValue];

        }
        Debug.Log("The rerolled correct pressure value is " + correctValuePressure);
        yield break;
    }
}
