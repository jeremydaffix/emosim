using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class contains an interactive object instance / agent
// defined by a model of interactive object (see class InteractiveObject)
public class InteractiveObjectInstance : MonoBehaviour
{
    public string InteractiveObjectName; // nom exposé éditeur unity

    float lookRange = 1.0f;


    InteractiveObject interactiveObject = null;


    int cptWalking = 0;
    Vector3 walkingDir = new Vector3();



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (interactiveObject == null && Environment.Instance.InteractiveObjects.ContainsKey(InteractiveObjectName))
            interactiveObject = Environment.Instance.InteractiveObjects[InteractiveObjectName];
    }




    private void FixedUpdate()
    {
        if (Simulation.Instance.Playing && Simulation.Instance.FrameCpt % Simulation.Instance.TurnDuration == 0) // 1 turn
        {
            int currentTurn = Simulation.Instance.FrameCpt / Simulation.Instance.TurnDuration;

            //Debug.Log("OBJECT " + interactiveObject.ObjectName + " TURN");

            if (interactiveObject.Type == InteractiveObject.TYPE_ANIMAL) // animal -> we walk
            {
                RandomWalk();


                if (interactiveObject.NeedsSatisfied["health"] < 0) // dangereous animal !!! o
                {
                    List<GameObject> l = LookForPersons(); // people too close -> attack!

                    foreach(GameObject person in l)
                    {
                        person.GetComponent<Person>().CognitiveMachine.Needs["health"].CurrentScore += interactiveObject.NeedsSatisfied["health"]; // ouch !
                        //Debug.Log("ATTACK");
                    }
                }
            }
            
        }
    }




    // randomly walk
    void RandomWalk(GameObject dontUse = null)
    {
        if (cptWalking <= 0)
        {
            // random direction
            int xDir = Random.Range(-1, 2);
            int yDir = Random.Range(-1, 2);

            walkingDir = new Vector3(xDir, yDir, 0);

            // random time
            cptWalking = Random.Range(4, 10);
        }


        Walk();
    }

    
    // walk in the direction calculated with RandomWalk()
    void Walk()
    {
        Vector3 initPos = transform.position;

        if (transform.position.x + walkingDir.x < -Environment.Instance.borderX || transform.position.x + walkingDir.x > Environment.Instance.borderX) walkingDir.x = walkingDir.x * -1;
        if (transform.position.y + walkingDir.y < -Environment.Instance.borderY || transform.position.y + walkingDir.y > Environment.Instance.borderY) walkingDir.y = walkingDir.y * -1;


        GetComponent<Rigidbody2D>().MovePosition(initPos + (walkingDir / 15f));

        cptWalking--;
    }


    // detect close persons
    public List<GameObject> LookForPersons()
    {
        List<GameObject> l = new List<GameObject>();

        foreach (Person p in Environment.Instance.Persons)
        {
            float dist = Vector3.Distance(p.transform.position, transform.position);

            if (dist <= LookRange) l.Add(p.gameObject);
        }

        return l;
    }



    // click -> display data about that object
    private void OnMouseDown()
    {
        UIController.Instance.DisplayedObject = this;
        UIController.Instance.DisplayedPerson = null;
    }





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

    public float LookRange
    {
        get
        {
            return lookRange;
        }

        set
        {
            lookRange = value;
        }
    }
}
