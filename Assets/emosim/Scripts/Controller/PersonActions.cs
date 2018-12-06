using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void PersonAction(GameObject go);


public class PersonActions
{
    public PersonAction /*ActionWalk,*/ ActionRandomWalk, ActionWalkToTarget, ActionEat, ActionFleeTarget;


    Person person;

    int cptWalking = 0;
    Vector3 walkingDir = new Vector3();

    LineRenderer lr;
    TextMesh tm;


    public PersonActions(Person p)
    {
        person = p;

        //ActionWalk = Walk;
        ActionRandomWalk = RandomWalk;
        ActionWalkToTarget = WalkToTarget;
        ActionEat = Eat;
        ActionFleeTarget = FleeTarget;

        lr = person.GetComponent<LineRenderer>();
        tm = person.GetComponentInChildren<TextMesh>();
    }


    // return all objects and persos we can see
    // TODO detect with more senses (smell,...)
    public List<GameObject> LookForThings()
    {
        List<GameObject> l = new List<GameObject>();

        foreach (Person p in Environment.Instance.Persons)
        {
            float dist = Vector3.Distance(p.transform.position, person.transform.position);

            if (dist <= person.LookRange) l.Add(p.gameObject);
        }


        foreach (InteractiveObjectInstance i in Environment.Instance.InteractiveObjectInstances)
        {
            float dist = Vector3.Distance(i.transform.position, person.transform.position);

            if (dist <= person.LookRange) l.Add(i.gameObject);
        }


        return l;
    }


    void ClearGraphics()
    {
        lr.positionCount = 0;
        tm.text = "";
    }


    // ****


    void RandomWalk(GameObject dontUse = null)
    {
        if (cptWalking <= 0)
        {
            // random direction
            int xDir = Random.Range(-1, 2);
            int yDir = Random.Range(-1, 2);

            walkingDir = new Vector3(xDir, yDir, 0);

            // random time
            cptWalking = Random.Range(1, 5);
        }


        Walk();
    }


    void WalkToTarget(GameObject target)
    {
        //Debug.Log("GOING TO " + target.GetComponent<InteractiveObjectInstance>().InteractiveObjectName);

        walkingDir = (target.transform.position - person.transform.position).normalized;
        cptWalking = 1;

        Walk();

        
        lr.positionCount = 2;
        lr.SetPosition(0, person.transform.position + (walkingDir / 2f));
        lr.SetPosition(1, target.transform.position);
    }

    void FleeTarget(GameObject target)
    {
        walkingDir = (target.transform.position - person.transform.position).normalized * -1;
        cptWalking = 1;

        Walk();
    }


    void Walk(GameObject dontUse = null)
    {
        if (person.transform.position.x + walkingDir.x < -Environment.Instance.borderX || person.transform.position.x + walkingDir.x > Environment.Instance.borderX) walkingDir.x = walkingDir.x * -1;
        if (person.transform.position.y + walkingDir.y < -Environment.Instance.borderY || person.transform.position.y + walkingDir.y > Environment.Instance.borderY) walkingDir.y = walkingDir.y * -1;


        person.GetComponent<Rigidbody2D>().MovePosition(person.transform.position + (walkingDir / 2f));

        cptWalking--;

        ClearGraphics();
    }


    void Eat(GameObject target)
    {
        InteractiveObjectInstance ioi = target.GetComponent<InteractiveObjectInstance>();

        if (ioi != null)
        {
            person.EmotionalMachine.ResetPerceptions();
            person.EmotionalMachine.TasteObject(ioi.InteractiveObject);
            person.EmotionalMachine.SaveInSomaticMemory(ioi.InteractiveObject);
            // needs ?

            Environment.Instance.RecycleInteractiveObject(ioi);
        }

        ClearGraphics();


        int score = person.EmotionalMachine.CalcMood();
        string txt;
        Color col;

        if (score < -5)
        {
            txt = "- -";
            col = new Color(1f, 0f, 0f);
        }

        else if (score < -2)
        {
            txt = " - ";
            col = new Color(1.0f, 0.5f, 0f);
        }

        else if (score < 3)
        {
            txt = "...";
            col = new Color(1f, 1f, 1f);
        }

        else if (score < 5)
        {
            txt = " + ";
            col = new Color(0f, 0.2f, 1.0f);
        }

        else
        {
            txt = "++";
            col = new Color(0.5f, 1f, 0f);
        }

        tm.text = txt;
        tm.color = col;
    }
}
