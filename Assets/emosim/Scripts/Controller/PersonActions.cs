using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public delegate void PersonAction(GameObject go); // type for a person's action


// this class contains the actions available for a person
public class PersonActions
{
    public PersonAction ActionRandomWalk, ActionWalkToTarget, ActionEat, ActionFleeTarget;


    Person person; // the person we are attached to

    // for walking
    int cptWalking = 0;
    Vector3 walkingDir = new Vector3();

    // for drawing things
    LineRenderer lr;
    TextMesh tm;



    public PersonActions(Person p)
    {
        person = p;

        ActionRandomWalk = RandomWalk;
        ActionWalkToTarget = WalkToTarget;
        ActionEat = Eat;
        ActionFleeTarget = FleeTarget;

        lr = person.GetComponent<LineRenderer>();
        tm = person.GetComponentInChildren<TextMesh>();
    }


    // return all objects and persos we can see
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
            cptWalking = Random.Range(0, 4);
        }


        Walk();
    }


    // walk towards a target
    void WalkToTarget(GameObject target)
    {
        //Debug.Log("GOING TO " + target.GetComponent<InteractiveObjectInstance>().InteractiveObjectName);

        walkingDir = (target.transform.position - person.transform.position).normalized;
        cptWalking = 1;

        Walk();

        lr.positionCount = 2;
        lr.startColor = new Color(0f,1f,0f);
        lr.endColor = new Color(0f, 1f, 0f);
        lr.SetPosition(0, person.transform.position + (walkingDir / 2f));
        lr.SetPosition(1, target.transform.position);

        // 8EF301
    }

    // flee a danger! (= walk faster in the opposite direction)
    void FleeTarget(GameObject target)
    {
        walkingDir = (target.transform.position - person.transform.position).normalized * -1.5f;
        cptWalking = 2;

        Walk();

        lr.positionCount = 2;
        lr.startColor = new Color(1f, 0f, 0f);
        lr.endColor = new Color(1f, 0f, 0f);
        lr.SetPosition(0, person.transform.position + (walkingDir / 2f));
        lr.SetPosition(1, target.transform.position);
    }


    // walk in the current direction
    void Walk(GameObject dontUse = null)
    {
        Vector3 initPos = person.transform.position;

        if (person.transform.position.x + walkingDir.x < -Environment.Instance.borderX || person.transform.position.x + walkingDir.x > Environment.Instance.borderX) walkingDir.x = walkingDir.x * -1;
        if (person.transform.position.y + walkingDir.y < -Environment.Instance.borderY || person.transform.position.y + walkingDir.y > Environment.Instance.borderY) walkingDir.y = walkingDir.y * -1;


        person.GetComponent<Rigidbody2D>().MovePosition(initPos + (walkingDir / 2f));

        cptWalking--;

        ClearGraphics();
    }


    // eat an object
    void Eat(GameObject target)
    {
        InteractiveObjectInstance ioi = target.GetComponent<InteractiveObjectInstance>();

        if (ioi != null)
        {
            // we trigger the perceptions and we save the result in the somatic memory
            person.EmotionalMachine.ResetPerceptions();
            person.EmotionalMachine.TasteObject(ioi.InteractiveObject);
            person.EmotionalMachine.SaveInSomaticMemory(ioi.InteractiveObject);


            // apply the effects to the needs
            foreach (KeyValuePair<string, float> kvp in ioi.InteractiveObject.NeedsSatisfied)
            {
                if (person.CognitiveMachine.Needs.ContainsKey(kvp.Key))
                {
                    Need n = person.CognitiveMachine.Needs[kvp.Key];

                    n.CurrentScore += kvp.Value;
                }
            }


            Environment.Instance.RecycleInteractiveObject(ioi); // move the object somewhere else
        }

        ClearGraphics();


        // display some emotional feedback

        int score = person.EmotionalMachine.CalcMood();
        //Debug.Log("EAT " + person.EmotionalMachine.LastEmotions.ElementAt(0).Key.Name + " ; " + person.EmotionalMachine.LastEmotions.ElementAt(1).Key.Name);

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
