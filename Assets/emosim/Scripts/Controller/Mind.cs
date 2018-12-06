
using System.Collections.Generic;
using UnityEngine;

public class Mind
{
    Person person;
    



    public Mind(Person p)
    {
        person = p;
    }


    public void TakeDecision()
    {
        //Debug.Log("PERSON TURN");

        //mind.TakeDecision();

        person.EmotionalMachine.CalcEmotionalActions();
        person.CognitiveMachine.CalcCognitiveActions();

        // merging the 2 lists of possible actions !

        Dictionary<GameObject, KeyValuePair<PersonAction, int>> mergedPossibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>(person.EmotionalMachine.PossibleActions);

       foreach (KeyValuePair<GameObject, KeyValuePair<PersonAction, int>> pa in person.CognitiveMachine.PossibleActions)
        {
            GameObject target = pa.Key;
            int cognitiveScore = pa.Value.Value; // * coef
            PersonAction cognitiveAction = pa.Value.Key;

            int mergedScore = cognitiveScore;
            PersonAction mergedAction = cognitiveAction;

            // only in cognitive list : we add it "as it is"


            if (mergedPossibleActions.ContainsKey(target)) // gameobject in both lists
            {
                int emoScore = mergedPossibleActions[target].Value;
                PersonAction emoAction = mergedPossibleActions[target].Key;

                // emotional action can be walkTo, eat or flee actions
                // cognitive can only be walkTo (with negative score, depending on the distance) or eat actions


                // opposites actions : in our case, the emotional part is willing to flee, while the cognitive part wants to walk towards or eat the object
                // we take the best score
                if (emoAction == person.PersonActions.ActionFleeTarget)
                {
                    if(emoScore >= cognitiveScore) // fear wins
                    {
                        mergedScore = emoScore;
                        mergedAction = emoAction;
                    }

                    else // need to eat wins
                    {
                        mergedScore -= emoScore; // but with a malus
                    }
                }

                // same action ish : we just add scores
                else
                {
                    mergedScore = cognitiveScore + emoScore;
                    mergedAction = emoAction;
                }

            }
            

            mergedPossibleActions[target] = new KeyValuePair<PersonAction, int>(mergedAction, mergedScore);

        }






        // select the best action to do !

        int bestScore = 0;
        GameObject bestTarget = person.gameObject;
        PersonAction bestAction = person.PersonActions.ActionRandomWalk;

        foreach (KeyValuePair<GameObject, KeyValuePair<PersonAction, int>> pa in mergedPossibleActions)
        {
            GameObject target = pa.Key;
            int score = pa.Value.Value;
            PersonAction action = pa.Value.Key;

            if (score > bestScore)
            {
                bestScore = score;
                bestTarget = target;
                bestAction = action;
            }
        }

        bestAction(bestTarget);
    }




    /*public void TakeDecision()
    {
        //Random.seed = System.Environment.TickCount;

        List <GameObject> canSee = person.PersonActions.LookForThings();


        GameObject bestTarget = null;
        int bestTargetScore = 0;

        LineRenderer lr = person.GetComponent<LineRenderer>();
        lr.positionCount = 0;

        TextMesh tm = person.GetComponentInChildren<TextMesh>();
        tm.text = "";


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
                    //Debug.Log("** SEE " + ioi.InteractiveObjectName);
                    person.EmotionalMachine.TestObject(ioi.InteractiveObject);

                    int score = person.EmotionalMachine.CalcMood();

                    if ((score > bestTargetScore) ||
                        (score == bestTargetScore && Vector3.Distance(person.transform.position, go.transform.position) < Vector3.Distance(person.transform.position, bestTarget.transform.position)))
                    {
                        bestTarget = go;
                        bestTargetScore = score;
                    }
                }
            }

            else // we see a person
            {
                // apprentissage ici !!!
            }
        }


        if(bestTargetScore > 0)
        {
            if (Vector3.Distance(person.transform.position, bestTarget.transform.position) <= 1f)
            {
                person.PersonActions.ActionEat(bestTarget);

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
                    col = new Color(0f, 0.5f, 1.0f);
                }

                else
                {
                    txt = "++";
                    col = new Color(0f, 1f, 0f);
                }

                tm.text = txt;
                tm.color = col;
            }

            else
            {
                person.PersonActions.ActionWalkToTarget(bestTarget);

                lr.positionCount = 2;
                lr.SetPosition(0, person.transform.position);
                lr.SetPosition(1, bestTarget.transform.position);
            }
            
        }


        else
        {
            // default behaviour :
            // random walking

            person.PersonActions.ActionRandomWalk(null);
        }
    }*/


    /*void CognitiveProcess()
    {

    }

    void EmotionalProcess()
    {

    }*/


    
}
