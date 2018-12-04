
using System.Collections.Generic;
using UnityEngine;

public class Mind
{
    Person person;



    int cptWalking = 0;
    Vector3 walkingDir = new Vector3();




    public Mind(Person p)
    {
        person = p;
    }


    public void TakeDecision()
    {

        // default behaviour :
        // random walking

        ActionRandomWalk();
    }

    void CognitiveProcess()
    {

    }

    void EmotionalProcess()
    {

    }



    // **********


    void ActionRandomWalk()
    {
        if (cptWalking <= 0)
        {
            

            // random direction
            int xDir = Random.Range(-1, 2);
            int yDir = Random.Range(-1, 2);

            walkingDir = new Vector3(xDir, yDir, 0);

            // random time
            cptWalking = Random.Range(1, 5);


            /*Debug.Log("*** WALK ***");
            Debug.Log(walkingDir);
            Debug.Log(cptWalking);*/
        }



        if (person.transform.position.x + walkingDir.x < -10 || person.transform.position.x + walkingDir.x > 10) walkingDir.x = walkingDir.x * -1;
        if (person.transform.position.y + walkingDir.y < -5 || person.transform.position.y + walkingDir.y > 5) walkingDir.y = walkingDir.y * -1;


        person.transform.position += (walkingDir / 2f);

        cptWalking--;
    }
}
