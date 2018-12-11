
using System.Collections.Generic;
using UnityEngine;

// this class represents the mind, and uses both an emotional and a cognitive part to make a decision
public class Mind
{
    Person person;
    



    public Mind(Person p)
    {
        person = p;
    }


    // calculate the best action to do, according to the current context (objects seen, needs,...)
    public KeyValuePair<PersonAction, GameObject> TakeDecision()
    {
        KeyValuePair<PersonAction, GameObject> actionToDo = new KeyValuePair<PersonAction, GameObject>();

        person.EmotionalMachine.CalcEmotionalActions(); // calc a list of possible actions (each with a "score"), using the emotional part
        person.CognitiveMachine.CalcCognitiveActions(); // calc a list of possible actions (each with a "score"), using the cognitive part


        // merging the 2 lists of possible actions !

        Dictionary<GameObject, KeyValuePair<PersonAction, int>> mergedPossibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>(person.EmotionalMachine.PossibleActions);

       foreach (KeyValuePair<GameObject, KeyValuePair<PersonAction, int>> pa in person.CognitiveMachine.PossibleActions)
        {
            GameObject target = pa.Key;
            int cognitiveScore = Mathf.RoundToInt(pa.Value.Value);
            PersonAction cognitiveAction = pa.Value.Key;

            int mergedScore = cognitiveScore;
            PersonAction mergedAction = cognitiveAction;

            // if only in cognitive list : we add it "as it is"


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

                    if (emoScore >= cognitiveScore)
                    //if(Mathf.Abs(emoScore) >= Mathf.Abs(cognitiveScore)) // fear wins
                    {
                        mergedScore = emoScore;
                        mergedAction = emoAction;
     
                    }

                    //else // need to eat wins
                    {
                        //mergedScore -= emoScore; // but with a malus
                    }
                }

                // same action ish : we just add scores
                else
                {
                    mergedScore = cognitiveScore + emoScore;
                    mergedAction = emoAction;
                }

            }


            // !!! paliatif temporaire pour bug sur l'impact des besoins urgents sur le choix
            // permet de ne pas manger des trucs mauvais pour la santé si on est en trop mauvaise santé
            // l'idéal serait que le "malus" dans cognitivemachine fonctionne sans bug

            if (person.CognitiveMachine.Needs["health"].CurrentScore < 5f
                && Simulation.Instance.CognitiveWeight > 0.5f &&
                target.GetComponent<InteractiveObjectInstance>().InteractiveObject.NeedsSatisfied["health"] < 0f)
                mergedScore = 0;

            if (person.CognitiveMachine.Needs["health"].CurrentScore < 5f
                && Simulation.Instance.CognitiveWeight > 0.5f &&
                target.GetComponent<InteractiveObjectInstance>().InteractiveObject.NeedsSatisfied["health"] >= 2f)
                            mergedScore *= 10;


            mergedPossibleActions[target] = new KeyValuePair<PersonAction, int>(mergedAction, mergedScore);

        }
       


        // select the best action to do ! (= best score)

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


        // "desirability map" enabled ?
        if (Simulation.Instance.DesirabilityView == person)
        {
            Environment.Instance.DisplayDesirabilityMap(mergedPossibleActions, bestScore);
        }

        //Debug.Log(bestAction.Method.Name + " -> " + bestScore);
        //if(bestTarget != null && bestTarget.GetComponent<InteractiveObjectInstance>() != null) Debug.Log("(" + bestTarget.GetComponent<InteractiveObjectInstance>().InteractiveObjectName + ")");


        actionToDo = new KeyValuePair<PersonAction, GameObject>(bestAction, bestTarget);
        
        return actionToDo; // aaand we return it as a pair : best action to do / object concerned

    }
    

    
}
