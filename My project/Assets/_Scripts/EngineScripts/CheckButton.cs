using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class CheckButton : MonoBehaviour
{

    //lights that will tell the player if they have the lever in the right position
    [SerializeField]private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
    [SerializeField] private GameObject light4;
    
    //materials for the lights
    [SerializeField] private Material correct;
    [SerializeField] private Material wrong;

    private int numberrodcorrect;

    //value storing if the rod should be up or down(on or off)
    bool rod1correctvalue;
    bool rod2correctvalue;
    bool rod3correctvalue;
    bool rod4correctvalue;

    //values storing if the rod was in the correct position last time the button was pressed
    bool rod1correct;
    bool rod2correct;
    bool rod3correct;
    bool rod4correct;

    // Reference to the main engine script
    private EngineControlScript enginecontrol;


    private void Start()
    {
        enginecontrol = Object.FindFirstObjectByType<EngineControlScript>();
        rod1correct = false;
        rod2correct = false;
        rod3correct = false;
        rod4correct = false;

        numberrodcorrect = 0;
        ChangeRods();
        StartCoroutine(enginecontrol.EngineTimer());
    }

    private void OnMouseDown()
    {
        if (enginecontrol.engineoverheated && !enginecontrol.reactorrodsokay) 
        {
            CheckEngine();
        }
        
    }


    private void CheckEngine()
    {

        if(enginecontrol.enginerod1on == rod1correctvalue)
        {
            light1.GetComponent<MeshRenderer>().material = correct;
            if (!rod1correct)
            {
                numberrodcorrect += 1;
                rod1correct = true;
            }
        }
        else
        {
            light1.GetComponent<MeshRenderer>().material = wrong;
            if (rod1correct)
            {
                numberrodcorrect -= 1;
                rod1correct = false;
            }
        }

        if(enginecontrol.enginerod2on == rod2correctvalue)
        {
            light2.GetComponent<MeshRenderer>().material = correct;
            if (!rod2correct)
            {
                numberrodcorrect += 1;
                rod2correct = true;
            }
        }
        else
        {
            light2.GetComponent<MeshRenderer>().material = wrong;
            if (rod2correct)
            {
                numberrodcorrect -= 1;
                rod2correct = false;
            }
        }

        if (enginecontrol.enginerod3on == rod3correctvalue)
        {
            light3.GetComponent<MeshRenderer>().material = correct;
            if (!rod3correct)
            {
                numberrodcorrect += 1;
                rod3correct = true;
            }
        }
        else
        {
            light3.GetComponent<MeshRenderer>().material = wrong;
            if (rod3correct)
            {
                numberrodcorrect -= 1;
                rod3correct = false;
            }

        }

        if (enginecontrol.enginerod4on == rod4correctvalue)
        {
            light4.GetComponent<MeshRenderer>().material = correct;
            if (!rod4correct)
            {
                numberrodcorrect += 1;
                rod4correct = true;
            }
        }
        else
        {
            light4.GetComponent<MeshRenderer>().material = wrong;
            if (rod4correct)
            {
                numberrodcorrect -= 1;
                rod4correct = false;
            }
        }

        if(numberrodcorrect == 4)
        {
            StartCoroutine(enginecontrol.EngineTimer());
            enginecontrol.reactorrodsokay = true;
            enginecontrol.engineoverheated = false;
            Debug.Log("all rods okay");
            enginecontrol.rod1levercollider.enabled = false;
            enginecontrol.rod2levercollider.enabled = false;
            enginecontrol.rod3levercollider.enabled = false;
            enginecontrol.rod4levercollider.enabled = false;

        }
    }

    public void ChangeRods()
    {
        //random.value returns a float between 0 and 1 and here each rod bool will only be true of the value is above .5 so a 50% chance
        rod1correctvalue = UnityEngine.Random.value > 0.5f;
        rod2correctvalue = UnityEngine.Random.value > 0.5f;
        rod3correctvalue = UnityEngine.Random.value > 0.5f;
        rod4correctvalue = UnityEngine.Random.value > 0.5f;
        numberrodcorrect = 0;
        rod1correct = false;
        rod2correct = false;
        rod3correct = false;
        rod4correct = false;
        if (enginecontrol.requireonerod)
        {
            if(!rod1correctvalue && !rod2correctvalue && !rod3correctvalue && !rod4correctvalue)
            {
                //random.range is inclusive for min number but exclusive for max number(meaning the min number is a possible output, but the max number is one number greater than the highest possible output)
                int rodnumber = Random.Range(1, 5);

                if (rodnumber == 1)
                {
                    rod1correctvalue = true;
                }
                else if(rodnumber == 2)
                {
                    rod2correctvalue = true;
                }
                else if(rodnumber == 3)
                {
                    rod3correctvalue = true;
                }
                else if(rodnumber == 4)
                {
                    rod4correctvalue = true;
                }
            }
        }

    }

    
}
