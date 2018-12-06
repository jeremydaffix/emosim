using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CognitiveMachine
{
    Person person;

    Dictionary<GameObject, KeyValuePair<PersonAction, int>> possibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>();



    public CognitiveMachine(Person p)
    {
        person = p;
    }



    public void AddPossibleAction(GameObject go, int score, PersonAction action)
    {
        PossibleActions.Add(go, new KeyValuePair<PersonAction, int>(action, score));
    }


    public void CalcCognitiveActions()
    {

        List<GameObject> canSee = person.PersonActions.LookForThings();


        PossibleActions.Clear();


        foreach (GameObject go in canSee)
        {
            InteractiveObjectInstance ioi = go.GetComponent<InteractiveObjectInstance>();
            Person p = go.GetComponent<Person>();

            if (ioi != null) // we see an object
            {
                if (ioi.InteractiveObject.Type == InteractiveObject.TYPE_OBSTACLE)
                {


                }

                else
                {
                    float dist = Vector3.Distance(person.transform.position, go.transform.position);
                    int score = -1 * (int)dist;

                    if(dist <= 1f)
                        AddPossibleAction(go, 1, person.PersonActions.ActionEat);

                    else
                        AddPossibleAction(go, score, person.PersonActions.ActionWalkToTarget);


                    // distance + needs + knowledge + pathfinding
                }
            }

            else // we see a person
            {
                // apprentissage ici !!!
            }
        }
        

    }


    public void TakeCognitiveDecision()
    {
        if(person.CollidingWith != null)
        {

        }
    }







    public Dictionary<GameObject, KeyValuePair<PersonAction, int>> PossibleActions
    {
        get
        {
            return possibleActions;
        }

        set
        {
            possibleActions = value;
        }
    }

}
