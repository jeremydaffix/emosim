using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonActions
{

    Person person;

    int cptWalking = 0;
    Vector3 walkingDir = new Vector3();


    public PersonActions(Person p)
    {
        person = p;
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




    public void ActionRandomWalk()
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


        ActionWalk();
    }


    public void ActionWalkToTarget(GameObject target)
    {
        //Debug.Log("GOING TO " + target.GetComponent<InteractiveObjectInstance>().InteractiveObjectName);

        walkingDir = (target.transform.position - person.transform.position).normalized;
        cptWalking = 1;

        ActionWalk();
    }


    public void ActionWalk()
    {
        if (person.transform.position.x + walkingDir.x < -Environment.Instance.borderX || person.transform.position.x + walkingDir.x > Environment.Instance.borderX) walkingDir.x = walkingDir.x * -1;
        if (person.transform.position.y + walkingDir.y < -Environment.Instance.borderY || person.transform.position.y + walkingDir.y > Environment.Instance.borderY) walkingDir.y = walkingDir.y * -1;


        person.GetComponent<Rigidbody2D>().MovePosition(person.transform.position + (walkingDir / 2f));

        cptWalking--;
    }


    public void ActionEat(GameObject target)
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

    }
}
