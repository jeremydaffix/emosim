
using System.Collections.Generic;
using UnityEngine;

public class Mind
{
    Person person;
    



    public Mind(Person p)
    {
        person = p;
    }


    public KeyValuePair<PersonAction, GameObject> TakeDecision()
    {
        KeyValuePair<PersonAction, GameObject> actionToDo = new KeyValuePair<PersonAction, GameObject>();

        //Debug.Log("PERSON TURN");

        //mind.TakeDecision();

        person.EmotionalMachine.CalcEmotionalActions();
        person.CognitiveMachine.CalcCognitiveActions();

        // merging the 2 lists of possible actions !

        Dictionary<GameObject, KeyValuePair<PersonAction, int>> mergedPossibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>(person.EmotionalMachine.PossibleActions);

       foreach (KeyValuePair<GameObject, KeyValuePair<PersonAction, int>> pa in person.CognitiveMachine.PossibleActions)
        {
            GameObject target = pa.Key;
            int cognitiveScore = Mathf.RoundToInt(pa.Value.Value * Simulation.Instance.RatioEmoCogn); // * coef
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


        //bestAction(bestTarget);


        actionToDo = new KeyValuePair<PersonAction, GameObject>(bestAction, bestTarget);

        return actionToDo;

    }
    

    
}
