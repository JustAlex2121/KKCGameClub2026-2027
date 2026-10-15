using UnityEngine;

public class ReactorRod : MonoBehaviour
{
    public UItest ui;
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Canvas");
        ui = objs[0].GetComponent<UItest>();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("trigger entered");
        if (trigger.CompareTag("On"))
        {
            ui.enginerodon = true;
        }
        else if (trigger.CompareTag("Off"))
        {
            ui.enginerodon = false;
        }
    }
}
