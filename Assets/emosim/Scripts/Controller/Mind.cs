
using System.Collections.Generic;
using UnityEngine;

public class Mind
{
    Person person;


    //List<>



    public Mind(Person p)
    {
        person = p;
    }


    public void TakeDecision()
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

            person.PersonActions.ActionRandomWalk();
        }
    }

    void CognitiveProcess()
    {

    }

    void EmotionalProcess()
    {

    }


    
}
