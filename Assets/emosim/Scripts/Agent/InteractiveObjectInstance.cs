using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractiveObjectInstance : MonoBehaviour
{
    public string InteractiveObjectName; // nom exposé éditeur unity


    InteractiveObject interactiveObject = null;

    public InteractiveObject InteractiveObject
    {
        get
        {
            return interactiveObject;
        }

        set
        {
            interactiveObject = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        if(Environment.Instance.InteractiveObjects.ContainsKey(InteractiveObjectName))
            interactiveObject = Environment.Instance.InteractiveObjects[InteractiveObjectName];
    }

    // Update is called once per frame
    void Update()
    {

    }




    private void FixedUpdate()
    {
        if (Simulation.Instance.TurnCpt % Simulation.Instance.TurnDuration == 0) // 1 turn
        {
            Debug.Log("OBJECT " + interactiveObject.ObjectName + " TURN");
        }
    }
}
