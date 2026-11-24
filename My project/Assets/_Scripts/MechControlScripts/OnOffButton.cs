using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class OnOffButton : MonoBehaviour
{

    [SerializeField]private ConsoleLevernew leftLever;
    [SerializeField] private ConsoleLevernew rightLever;
    [SerializeField] private EngineControlScript engine;

    public bool isOn = false;




    //mech movement variables
    public float mechspeedmultiplier;

    public float turnspeed;
    private float finalturnspeed;
    [SerializeField] private Rigidbody mechrb;


    private void Start()
    {
        isOn = false;
    }
    private void FixedUpdate()
    {
        if (isOn)
        {

            //forward and back movement
            if(leftLever.isUp && leftLever.isUp)
            {
                mechrb.angularVelocity = new Vector3(0, 0f, 0f);
                //allows the mech on the map to move on the X and Z axis
                mechrb.constraints &= ~RigidbodyConstraints.FreezePositionX;
                mechrb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

                Vector3 localDirection = new Vector3(0f, 0f, 1f);
                Vector3 globalDirection = mechrb.rotation * localDirection;
                Vector3 targetVelocity = globalDirection * mechspeedmultiplier;
                mechrb.linearVelocity = targetVelocity;
            }
            else if((!leftLever.isUp && !leftLever.isNeutral) && (!rightLever.isUp && !rightLever.isNeutral))
            {
                mechrb.angularVelocity = new Vector3(0, 0f, 0f);
                //allows the mech on the map to move on the X and Z axis
                mechrb.constraints &= ~RigidbodyConstraints.FreezePositionX;
                mechrb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

                Vector3 localDirection = new Vector3(0f, 0f, -1f);
                Vector3 globalDirection = mechrb.rotation * localDirection;
                Vector3 targetVelocity = globalDirection * mechspeedmultiplier;
                mechrb.linearVelocity = targetVelocity;
            }


            //turning
            if(leftLever.isUp && (!rightLever.isUp && !rightLever.isNeutral))
            {
                mechrb.linearVelocity = Vector3.zero;
                //allows for mech on map to rotate on the y axis
                mechrb.constraints &= ~RigidbodyConstraints.FreezeRotationY;

                //future proofing for later addition of making the amount the lever is pulled or pushed influence how fast the mech turns or moves
                finalturnspeed = turnspeed * -1;

                if (finalturnspeed > 0)
                {
                    finalturnspeed *= -1;
                }
                mechrb.angularVelocity = new Vector3(0f, finalturnspeed, 0f);
            }
            else if((!leftLever.isUp && !leftLever.isNeutral) && rightLever.isUp)
            {
                mechrb.linearVelocity = Vector3.zero;
                //allows for mech on map to rotate on the y axis
                mechrb.constraints &= ~RigidbodyConstraints.FreezeRotationY;

                //future proofing for later addition of making the amount the lever is pulled or pushed influence how fast the mech turns or moves
                finalturnspeed = turnspeed * 1;

                if (finalturnspeed < 0)
                {
                    finalturnspeed *= -1;
                }

                mechrb.angularVelocity = new Vector3(0f, finalturnspeed, 0f);
            }

            if (leftLever.isNeutral || rightLever.isNeutral)
            {

                //stops the mech on the map from being able to rotate on the y axis
                mechrb.constraints |= RigidbodyConstraints.FreezeRotationY;


                //stops the mech on the map from being able to move on the X and Y axis
                mechrb.constraints |= RigidbodyConstraints.FreezePositionX;
                mechrb.constraints |= RigidbodyConstraints.FreezePositionZ;

                //sets the mech's velocity to zero when either of the levers is neither up or down
                mechrb.linearVelocity = Vector3.zero;
                mechrb.angularVelocity = new Vector3(0, 0f, 0f);
            }
            
        }
        else
        {
            leftLever.EngineOffBehavior();
            rightLever.EngineOffBehavior();
            //stops the mech on the map from being able to rotate on the y axis
            mechrb.constraints |= RigidbodyConstraints.FreezeRotationY;


            //stops the mech on the map from being able to move on the X and Y axis
            mechrb.constraints |= RigidbodyConstraints.FreezePositionX;
            mechrb.constraints |= RigidbodyConstraints.FreezePositionZ;

            //sets the mech's velocity to zero when either of the levers is neither up or down
            mechrb.linearVelocity = Vector3.zero;
            mechrb.angularVelocity = new Vector3(0, 0f, 0f);
        }

    }
    private void OnMouseDown()
    {
        if (!engine.engineoverheated)
        {
            isOn = !isOn;
        }
        

        if (isOn)
        {
            Debug.Log("Mech control on");
        }
        else if (!isOn)
        {
            Debug.Log("Mech control off");
        }
    }
}
