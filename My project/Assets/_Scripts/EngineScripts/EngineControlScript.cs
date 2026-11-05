using UnityEngine;
using System.Collections;

public class EngineControlScript : MonoBehaviour
{

    public bool engineoverheated;

    [Header("How long before the engine overheats")]
    public float enginetimer;

    //value storing if the rods are up or down(on or off)
    public bool enginerod1on;
    public bool enginerod2on;
    public bool enginerod3on;
    public bool enginerod4on;
    [Header("Enable to require at least one rod to be active")]
    public bool requireonerod;

    //tried to use if statement in the onmousedown in the enginelever script to check if the rods in the engine were fine and if they were restrict movement, but that didn't work(couldn't figure out why)
    [Header("The colliders for the movable part of the engine")]
    public Collider rod1levercollider;
    public Collider rod2levercollider;
    public Collider rod3levercollider;
    public Collider rod4levercollider;
    public Collider coolantdialcollider;
    public Collider pressuredialcollider;

    //value for checking if the rods have already been put into the right position(not useful currently will be later when the rest of the engine stuff is implimented)
    public bool reactorrodsokay;
    public bool enginedialsokay;


    //reference to the CheckButton script
    private CheckButtonLever button;
    private CheckButtonDial dial;

    //materials for the lights
    public Material correct;
    public Material wrong;


    private void Start()
    {
        button = Object.FindFirstObjectByType<CheckButtonLever>();
        dial = Object.FindFirstObjectByType<CheckButtonDial>();

        rod1levercollider.enabled = false;
        rod2levercollider.enabled = false;
        rod3levercollider.enabled = false;
        rod4levercollider.enabled = false;
        coolantdialcollider.enabled = false;
        pressuredialcollider.enabled = false;

        reactorrodsokay = true;
        enginedialsokay = true;

        enginerod1on = false;
        enginerod2on = false;
        enginerod3on = false;
        enginerod4on = false;
    }

    public void CheckEngineStatus()
    {
        if(reactorrodsokay && enginedialsokay)
        {
            engineoverheated = false;
            StartCoroutine(EngineTimer());
        }
    }

    public IEnumerator EngineTimer()
    {
        Debug.Log("engine timer started");
        float ElapsedTime = 0;

        while (ElapsedTime < enginetimer)
        {
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        reactorrodsokay = false;
        enginedialsokay = false;
        Debug.Log("engine overheated");
        engineoverheated = true;
        rod1levercollider.enabled = true;
        rod2levercollider.enabled = true;
        rod3levercollider.enabled = true;
        rod4levercollider.enabled = true;
        coolantdialcollider.enabled = true;
        pressuredialcollider.enabled = true;
        button.ChangeRods();
        dial.ChangeValue();
        button.rodlight1.GetComponent<MeshRenderer>().material = wrong;
        button.rodlight2.GetComponent<MeshRenderer>().material = wrong;
        button.rodlight3.GetComponent<MeshRenderer>().material = wrong;
        button.rodlight4.GetComponent<MeshRenderer>().material = wrong;
        dial.lightcoolant.GetComponent<MeshRenderer>().material = wrong;
        dial.lightpressure.GetComponent<MeshRenderer>().material = wrong;
        yield break;
    }
}
