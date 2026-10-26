using UnityEngine;

public class ReactorRod : MonoBehaviour
{
    public UItest ui;
    [SerializeField]private CheckButton button;

    [Range(1, 4)] public int rodNumber;

    // Reference to the main engine script
    private EngineControlScript enginecontrol;


    private void Start()
    {
        enginecontrol = Object.FindFirstObjectByType<EngineControlScript>();
    }
    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("trigger entered");
        if (trigger.CompareTag("On"))
        {
            if (rodNumber == 1)
            {
                enginecontrol.enginerod1on = true;
            }
            else if (rodNumber == 2)
            {
                enginecontrol.enginerod2on = true;
            }
            else if(rodNumber == 3)
            {
                enginecontrol.enginerod3on = true;
            }
            else if(rodNumber == 4)
            {
                enginecontrol.enginerod4on = true;
            }
            
        }
        else if (trigger.CompareTag("Off"))
        {
            if (rodNumber == 1)
            {
                enginecontrol.enginerod1on = false;
            }
            else if (rodNumber == 2)
            {
                enginecontrol.enginerod2on = false;
            }
            else if (rodNumber == 3)
            {
                enginecontrol.enginerod3on = false;
            }
            else if (rodNumber == 4)
            {
                enginecontrol.enginerod4on = false;
            }
        }
    }
}
