using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CognitiveMachine
{
    Person person;

    Dictionary<GameObject, KeyValuePair<PersonAction, int>> possibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>();


    int cptAvoidObstacle = 0;



    public CognitiveMachine(Person p)
    {
        person = p;
    }



    public void AddPossibleAction(GameObject go, int score, PersonAction action)
    {
        //if (score < 0) return;

        PossibleActions.Add(go, new KeyValuePair<PersonAction, int>(action, Mathf.RoundToInt(score * Simulation.Instance.CognitiveWeight)));
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
                    int score = 0;


                    // weight with needs (KNOWLEDGE of what is "good" for you)

                    foreach (KeyValuePair<string, Need> kvp in person.EmotionalMachine.Needs)
                    {
                        if (kvp.Value.CurrentScore < 5f) // need trigger!
                        {
                            float sat = ioi.InteractiveObject.NeedsSatisfied[kvp.Key];
                            score += Mathf.RoundToInt(sat * sat * sat * 5); // the most the need is satisfied by the object, bigger the score
                            //Debug.Log("ADDING " + ioi.InteractiveObjectName + " " + score + " for " + kvp.Key); // attention au changement de signe
                        }
                    }



                    // weight with distance

                   /* float dist = Vector3.Distance(person.transform.position, go.transform.position);
                    score += (-1 * (int)dist);



                    if (dist <= 1f)
                        AddPossibleAction(go, 1, person.PersonActions.ActionEat);

                    else
                        AddPossibleAction(go, score, person.PersonActions.ActionWalkToTarget);*/

                    //Debug.Log(score);
                    // distance + needs + knowledge + pathfinding
                }
            }

            else // we see a person
            {
                // apprentissage ici !!!
            }
        }



        // collisions for obstacles avoidance

        // colliding with obstacle
        /*if (person.CollidingWith != null)
        {
            int nbrTurnsSinceCollision = (Simulation.Instance.FrameCpt - person.CollidingSince) / Simulation.Instance.TurnDuration;

            if (nbrTurnsSinceCollision > 1)
            {
                //AddPossibleAction(person.CollidingWith, nbrTurnsSinceCollision, person.PersonActions.ActionFleeTarget);
                AddPossibleAction(person.CollidingWith, nbrTurnsSinceCollision * nbrTurnsSinceCollision, person.PersonActions.ActionRandomWalk);
                //cptAvoidObstacle = 3;
            }
        }*/


        // no collision but blocked
        /*else*/ if (person.IsBlockedSince != 0)
        {
            int nbrTurnsSinceBlocked = (Simulation.Instance.FrameCpt - person.IsBlockedSince) / Simulation.Instance.TurnDuration;

            if (nbrTurnsSinceBlocked > 2)
            {
                //AddPossibleAction(person.gameObject, nbrTurnsSinceBlocked, person.PersonActions.ActionRandomWalk);
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

}
