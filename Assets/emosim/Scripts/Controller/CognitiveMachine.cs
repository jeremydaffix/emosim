using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// this class is the cognitive part of the mind
// it handles things like needs, obstacles, distances, urgent needs that have to be fulfilled, impacts of an object on needs,...
public class CognitiveMachine
{
    Person person;

    Dictionary<string, Need> needs = new Dictionary<string, Need>(); // needs (health, satiety,...)

    Dictionary<GameObject, KeyValuePair<PersonAction, int>> possibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>(); // possible actions (returned to mind)


    int cptAvoidObstacle = 0;




    public void Create(Person p)
    {
        person = p;


        // needs

        Needs["health"] = new Need("health", 10f, 0.01f);
        Needs["satiety"] = new Need("satiety", 5f, 0.04f);
    }


    public void AddPossibleAction(GameObject go, int score, PersonAction action)
    {
        //if (score < 0) return;

        PossibleActions.Add(go, new KeyValuePair<PersonAction, int>(action, Mathf.RoundToInt(score * Simulation.Instance.CognitiveWeight)));
    }


    // calculate all possible actions and return the list for the mind
    public void CalcCognitiveActions()
    {

        List<GameObject> canSee = person.PersonActions.LookForThings(); // what can we see?


        PossibleActions.Clear();


        foreach (GameObject go in canSee) // for each object we see
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
                    int score = 0;


                    // weight with needs (KNOWLEDGE of what is "good" for you)

                    foreach (KeyValuePair<string, Need> kvp in Needs)
                    {
                        float sat = ioi.InteractiveObject.NeedsSatisfied[kvp.Key];

                        if (kvp.Value.CurrentScore < 5f /*&& sat > 0f*/) // need trigger!
                        {
                            score += Mathf.RoundToInt(sat * sat * sat * 10f); // the most the need is satisfied by the object, bigger the score
                            //Debug.Log("ADDING " + ioi.InteractiveObjectName + " " + score + " for " + kvp.Key); // attention au changement de signe
                        }
                    }



                    // weight with distance (further -> less attractive)

                    float dist = Vector3.Distance(person.transform.position, go.transform.position);
                    score += (-1 * (int)dist);



                    if (dist <= 1f)
                        AddPossibleAction(go, 1, person.PersonActions.ActionEat);

                    else
                        AddPossibleAction(go, score, person.PersonActions.ActionWalkToTarget);

                    //Debug.Log(score);
                    // distance + needs + knowledge + pathfinding
                }
            }

            else // we see a person
            {
            }
        }




        // blocked : some random walk please
        if (person.IsBlockedSince != 0)
        {
            int nbrTurnsSinceBlocked = (Simulation.Instance.FrameCpt - person.IsBlockedSince) / Simulation.Instance.TurnDuration;

            if (nbrTurnsSinceBlocked > 2)
            {
                AddPossibleAction(person.gameObject, nbrTurnsSinceBlocked * nbrTurnsSinceBlocked, person.PersonActions.ActionRandomWalk);
                cptAvoidObstacle = 3;
                //Debug.Log("BLOCKED");
            }
        }


        // not colliding anymore / unblocked but we have to keep walking some turns to do a successful avoidance
        else if(cptAvoidObstacle > 0)
        {
            //Debug.Log("CPTBLOCKED");
            AddPossibleAction(person.gameObject, 20, person.PersonActions.ActionRandomWalk);
            cptAvoidObstacle--;
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

    public Dictionary<string, Need> Needs
    {
        get
        {
            return needs;
        }

        set
        {
            needs = value;
        }
    }
}
