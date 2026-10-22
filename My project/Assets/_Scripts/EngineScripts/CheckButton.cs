using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class CheckButton : MonoBehaviour
{
    private bool engineoverheated;
    [SerializeField] private float enginetimer;

    //value storing if the rods are up or down(on or off)
    public bool enginerod1on;
    public bool enginerod2on;
    public bool enginerod3on;
    public bool enginerod4on;

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

    //tried to use if statement in the onmousedown in the enginelever script to check if the rods in the engine were fine and if they were restrict movement, but that didn't work(couldn't figure out why)
    [SerializeField] private Collider rod1levercollider;
    [SerializeField] private Collider rod2levercollider;
    [SerializeField] private Collider rod3levercollider;
    [SerializeField] private Collider rod4levercollider;


    //value for checking if the rods have already been put into the right position(not useful currently will be later when the rest of the engine stuff is implimented)
    public bool reactorrodsokay;


    private void Start()
    {
        rod1levercollider.enabled = false;
        rod2levercollider.enabled = false;
        rod3levercollider.enabled = false;
        rod4levercollider.enabled = false;

        reactorrodsokay = true;

        enginerod1on = false;
        enginerod2on = false;
        enginerod3on = false;
        enginerod4on = false;

        rod1correct = false;
        rod2correct = false;
        rod3correct = false;
        rod4correct = false;

        numberrodcorrect = 0;
        ChangeRods();
        StartCoroutine(EngineTimer());
    }

    private void OnMouseDown()
    {
        if (engineoverheated && !reactorrodsokay) 
        {
            CheckEngine();
        }
        
    }


    private void CheckEngine()
    {

        if(enginerod1on == rod1correctvalue)
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

        if(enginerod2on == rod2correctvalue)
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

        if (enginerod3on == rod3correctvalue)
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

        if (enginerod4on == rod4correctvalue)
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
            StartCoroutine(EngineTimer());
            reactorrodsokay = true;
            engineoverheated = false;
            Debug.Log("all rods okay");
            rod1levercollider.enabled = false;
            rod2levercollider.enabled = false;
            rod3levercollider.enabled = false;
            rod4levercollider.enabled = false;

        }
    }

    private void ChangeRods()
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
    }

    private IEnumerator EngineTimer()
    {
        Debug.Log("engine timer started");
        float ElapsedTime = 0;

        while(ElapsedTime < enginetimer)
        {
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        reactorrodsokay = false;
        Debug.Log("engine overheated");
        engineoverheated = true;
        rod1levercollider.enabled = true;
        rod2levercollider.enabled = true;
        rod3levercollider.enabled = true;
        rod4levercollider.enabled = true;
        ChangeRods();
    }
}
