using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// this class is the emotional part of the mind
// it handles things like perceptions, emotions, somatic markers, target choice according to
// emotional considerations, fleeing a dangerous thing,...
public class EmotionalMachine
{

    Person person;

    List<SomaticMarker> somaticMemory = new List<SomaticMarker>(); // list of all somatic markers (memory of an object -> set of physical perceptions)

    Dictionary<string, Perception> perceptions = new Dictionary<string, Perception>(); // all types of perceptions (stomach ach, fast or slow heart beat, pain hormones,...)
    Dictionary<string, Emotion> emotions = new Dictionary<string, Emotion>(); // definitions of emotions

    Dictionary<Emotion, int> lastEmotions = new Dictionary<Emotion, int>(); // last emotions calculated


     Dictionary<GameObject, KeyValuePair<PersonAction, int>> possibleActions = new Dictionary<GameObject, KeyValuePair<PersonAction, int>>(); // possible actions (returned to mind)


    public void Create(Person p)
    {
        person = p;


        // création des différents éléments (perceptions, émotions,...)
        // + tard : chargement dans des fichiers de config !


        // perceptions
        // a perception = an organ, in a given state!

        Perceptions["normalHeartBeat"] = new Perception(person.Heart, 0);
        Perceptions["fastHeartBeat"] = new Perception(person.Heart, 1);
        Perceptions["slowHeartBeat"] = new Perception(person.Heart, 2);

        Perceptions["stomachNormal"] = new Perception(person.Stomach, 0);
        Perceptions["stomachAche"] = new Perception(person.Stomach, 1);
        Perceptions["wantToVomit"] = new Perception(person.Stomach, 2);

        Perceptions["noHormones"] = new Perception(person.Brain, 0);
        Perceptions["painHormones"] = new Perception(person.Brain, 1);
        Perceptions["pleasureHormones"] = new Perception(person.Brain, 2);
        Perceptions["stressHormones"] = new Perception(person.Brain, 3);
        Perceptions["relaxationHormones"] = new Perception(person.Brain, 4);

        Perceptions["pokerFace"] = new Perception(person.Face, 0);
        Perceptions["smilingFace"] = new Perception(person.Face, 1);
        Perceptions["sadFace"] = new Perception(person.Face, 2);
        Perceptions["frightenedFace"] = new Perception(person.Face, 3);

        Perceptions["relaxedPosture"] = new Perception(person.Posture, 0);
        Perceptions["closedFists"] = new Perception(person.Posture, 1);
        Perceptions["closedPosture"] = new Perception(person.Posture, 2);
        Perceptions["frightenedPosture"] = new Perception(person.Posture, 3);
        Perceptions["happyPosture"] = new Perception(person.Posture, 4);


        Perceptions["normalEyes"] = new Perception(person.Eyes, 0);
        Perceptions["inTears"] = new Perception(person.Eyes, 1);


        Perceptions["looksNormal"] = new Perception(person.EyesSensor, 0);
        Perceptions["looksGood"] = new Perception(person.EyesSensor, 1);
        Perceptions["looksBad"] = new Perception(person.EyesSensor, 2);
        Perceptions["looksTerrifying"] = new Perception(person.EyesSensor, 3);

        Perceptions["smellsNormal"] = new Perception(person.NoseSensor, 0);
        Perceptions["smellsGood"] = new Perception(person.NoseSensor, 1);
        Perceptions["smellsBad"] = new Perception(person.NoseSensor, 2);

        Perceptions["tastesNormal"] = new Perception(person.PalateSensor, 0);
        Perceptions["tastesGood"] = new Perception(person.PalateSensor, 1);
        Perceptions["tastesBad"] = new Perception(person.PalateSensor, 2);



        // emotions
        // an emotion = a set of perceptions

        emotions["fear"] = new Emotion("fear", -3);
        emotions["fear"].AddPerception(Perceptions["fastHeartBeat"]).AddPerception(Perceptions["stomachAche"]).AddPerception(Perceptions["normalEyes"]).AddPerception(Perceptions["stressHormones"]).AddPerception(Perceptions["frightenedFace"]).AddPerception(Perceptions["frightenedPosture"]).AddPerception(Perceptions["looksTerrifying"]);

        emotions["pleasure"] = new Emotion("pleasure", 2);
        emotions["pleasure"].AddPerception(Perceptions["fastHeartBeat"]).AddPerception(Perceptions["stomachNormal"]).AddPerception(Perceptions["normalEyes"]).AddPerception(Perceptions["pleasureHormones"]).AddPerception(Perceptions["smilingFace"]).AddPerception(Perceptions["happyPosture"]).AddPerception(Perceptions["looksGood"]).AddPerception(Perceptions["tastesGood"]).AddPerception(Perceptions["smellsGood"]);

        emotions["pain"] = new Emotion("pain", -1);
        emotions["pain"].AddPerception(Perceptions["fastHeartBeat"]).AddPerception(Perceptions["stomachNormal"]).AddPerception(Perceptions["inTears"]).AddPerception(Perceptions["painHormones"]).AddPerception(Perceptions["sadFace"]).AddPerception(Perceptions["closedFists"]);

        emotions["sadness"] = new Emotion("sadness", -1);
        emotions["sadness"].AddPerception(Perceptions["slowHeartBeat"]).AddPerception(Perceptions["stomachAche"]).AddPerception(Perceptions["inTears"]).AddPerception(Perceptions["painHormones"]).AddPerception(Perceptions["sadFace"]).AddPerception(Perceptions["closedPosture"]).AddPerception(Perceptions["smellsBad"]).AddPerception(Perceptions["looksBad"]).AddPerception(Perceptions["tastesBad"]);

        emotions["stress"] = new Emotion("stress", -1);
        emotions["stress"].AddPerception(Perceptions["fastHeartBeat"]).AddPerception(Perceptions["stomachAche"]).AddPerception(Perceptions["normalEyes"]).AddPerception(Perceptions["stressHormones"]).AddPerception(Perceptions["sadFace"]).AddPerception(Perceptions["closedPosture"]);

        emotions["relaxation"] = new Emotion("relaxation", 1);
        emotions["relaxation"].AddPerception(Perceptions["slowHeartBeat"]).AddPerception(Perceptions["stomachNormal"]).AddPerception(Perceptions["normalEyes"]).AddPerception(Perceptions["relaxationHormones"]).AddPerception(Perceptions["smilingFace"]).AddPerception(Perceptions["relaxedPosture"]);

        emotions["sickness"] = new Emotion("sickness", -2);
        emotions["sickness"].AddPerception(Perceptions["normalHeartBeat"]).AddPerception(Perceptions["wantToVomit"]).AddPerception(Perceptions["normalEyes"]).AddPerception(Perceptions["painHormones"]).AddPerception(Perceptions["sadFace"]).AddPerception(Perceptions["closedPosture"]);

        emotions["neutral"] = new Emotion("neutral", 0);
        emotions["neutral"].AddPerception(Perceptions["normalHeartBeat"]).AddPerception(Perceptions["stomachNormal"]).AddPerception(Perceptions["normalEyes"]).AddPerception(Perceptions["noHormones"]).AddPerception(Perceptions["pokerFace"]).AddPerception(Perceptions["relaxedPosture"]);

        


        // innate somatic markers

        if (Simulation.Instance.InnateEnabled)
        {
            // example : an amanita is beautiful, but POISONOUS
            SomaticMarker amanitaSM = new SomaticMarker(new MentalImage(MentalImage.TYPE_OBJECT, Environment.Instance.InteractiveObjects["amanita"]));
            amanitaSM.Perceptions.Add(Perceptions["wantToVomit"]);
            amanitaSM.Perceptions.Add(Perceptions["painHormones"]);
            SomaticMemory.Add(amanitaSM);
        }

        

    }


    // calculate a "global mood", according to the emotions we are currently feeling
    public int CalcMood()
    {
        Dictionary<Emotion, int> em = CalcEmotions();

        lastEmotions = em;

        int score = 0;

        foreach(KeyValuePair<Emotion, int> kvp in em) // for each emotion calculated
        {
            if (kvp.Value >= (em.Values.Max() / 3f))
            {
                //Debug.Log(kvp.Key.Name + " : " + kvp.Value);

                score += (kvp.Key.DesirabilityScore * kvp.Value);
            }
        }

        //Debug.Log("MOOD : " + score);

       

        return score;
    }


    // calculates the "most likely" emotions, according to the states of each organs (= physical perceptions)
    public Dictionary<Emotion, int> CalcEmotions()
    {
        Dictionary<Emotion, int> em = new Dictionary<Emotion, int>();

        // for each possible emotion we calculate a score
        foreach(Emotion e in emotions.Values)
        {
            int score = 0;

            // for each perception linked to the current emotion
            foreach(Perception p in e.Perceptions) // increase score if the organ is in the right state
            {
                if(p.Organ.State == p.State)
                {
                    score++;
                }
            }

            em[e] = score;
        }

        // order the list (best score = most likely first)
        em = em.OrderByDescending(u => u.Value).ToDictionary(z => z.Key, y => y.Value);

        return em;
    }



    // trigger the perceptions by seeing an object
    public void SeeObject(InteractiveObject obj)
    {
        foreach(string s in obj.TriggerBySight)
        {
            if(Perceptions.ContainsKey(s))
            {
                Perception p = Perceptions[s];
                
                p.Organ.State = p.State;
            }
        }
    }

    // trigger the perceptions by smelling an object
    public void SmellObject(InteractiveObject obj)
    {
        foreach (string s in obj.TriggerBySmell)
        {
            if (Perceptions.ContainsKey(s))
            {
                Perception p = Perceptions[s];

                p.Organ.State = p.State;
            }
        }
    }

    // trigger the perceptions by tasting / eating an object
    public void TasteObject(InteractiveObject obj)
    {
        if (Simulation.Instance.InnateEnabled)
        {
            foreach (string s in obj.TriggerByTaste)
            {
                if (Perceptions.ContainsKey(s))
                {
                    Perception p = Perceptions[s];

                    p.Organ.State = p.State;
                }
            }
        }
    }


    // trigger the perceptions by remembering (somatic memory) an object
    public void RememberObject(InteractiveObject obj)
    {
        // note : a same object can have multiple entries in the memory !!!
        // until we implement strength, we take the last
        foreach(SomaticMarker sm in somaticMemory)
        {
            if(sm.MentalImage.Type == MentalImage.TYPE_OBJECT && sm.MentalImage.ObjectRemembered == obj)
            {
                foreach (Perception p in sm.Perceptions)
                {
                    p.Organ.State = p.State;
                }
            }
            
        }
    }



    // trigger different perceptions (smelling, seeing, remembering)
    // because we are near an object
    // this will be used to calculate emotions
    public void TestObject(InteractiveObject obj)
    {
        person.EmotionalMachine.ResetPerceptions();

        if (Simulation.Instance.InnateEnabled)
        {
            person.EmotionalMachine.SeeObject(obj);
            person.EmotionalMachine.SmellObject(obj);
        }

        person.EmotionalMachine.RememberObject(obj);
    }


    // reset all perceptions
    public void ResetPerceptions()
    {
        person.Brain.State = 0;
        person.Eyes.State = 0;
        person.EyesSensor.State = 0;
        person.Face.State = 0;
        person.Heart.State = 0;
        person.NoseSensor.State = 0;
        person.PalateSensor.State = 0;
        person.Posture.State = 0;
        person.Stomach.State = 0;
    }





    // save the memory of a given (model of) interactive object
    public void SaveInSomaticMemory(InteractiveObject io)
    {
        SomaticMarker sm = new SomaticMarker(new MentalImage(MentalImage.TYPE_OBJECT, io), 3);

        sm.Perceptions.Add(new Perception(person.Brain, person.Brain.State));
        sm.Perceptions.Add(new Perception(person.Stomach, person.Stomach.State));
        sm.Perceptions.Add(new Perception(person.Eyes, person.Eyes.State));
        sm.Perceptions.Add(new Perception(person.Heart, person.Heart.State));
        sm.Perceptions.Add(new Perception(person.Posture, person.Posture.State));
        sm.Perceptions.Add(new Perception(person.Face, person.Face.State));

        sm.Perceptions.Add(new Perception(person.EyesSensor, person.EyesSensor.State));
        sm.Perceptions.Add(new Perception(person.NoseSensor, person.NoseSensor.State));
        sm.Perceptions.Add(new Perception(person.PalateSensor, person.PalateSensor.State));

        SomaticMemory.Add(sm);
    }



    public void AddPossibleAction(GameObject go, int score, PersonAction action)
    {
        //if (score < 0) return;

        PossibleActions.Add(go, new KeyValuePair<PersonAction, int>(action, Mathf.RoundToInt(score * Simulation.Instance.EmotionalWeight)));
    }


    // calculate all possible actions and return the list for the mind
    public void CalcEmotionalActions()
    {

        List<GameObject> canSee = person.PersonActions.LookForThings(); // what can we see?


        PossibleActions.Clear();


        foreach (GameObject go in canSee) // for each object we can see
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
                    person.EmotionalMachine.TestObject(ioi.InteractiveObject); // "look at" and "smell" the object, activate perceptions

                    int score = person.EmotionalMachine.CalcMood(); // according to the triggered perceptions : does this object make me feel good? calculate a score

                    // fear : flee action
                    if (lastEmotions.ElementAt(0).Key == emotions["fear"] && Vector3.Distance(person.transform.position, go.transform.position) <= (person.LookRange / 1.8f))
                    {
                        AddPossibleAction(go, Mathf.RoundToInt(20f * Simulation.Instance.EmotionalWeight), person.PersonActions.ActionFleeTarget);
                    }

                    // we are close enough to eat the object
                    else if (Vector3.Distance(person.transform.position, go.transform.position) <= 1f)
                    {
                        AddPossibleAction(go, score, person.PersonActions.ActionEat);
                    }

                    // walk towards the object action
                    else
                    {
                        AddPossibleAction(go, score, person.PersonActions.ActionWalkToTarget);
                    }
                        
                }
            }

            else // we see a person
            {
                // apprentissage ici !!!
            }
        }

        
    }





    public List<SomaticMarker> SomaticMemory
    {
        get
        {
            return somaticMemory;
        }

        set
        {
            somaticMemory = value;
        }
    }

    public Dictionary<string, Emotion> Emotions
    {
        get
        {
            return emotions;
        }

        set
        {
            emotions = value;
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

    public Dictionary<Emotion, int> LastEmotions
    {
        get
        {
            return lastEmotions;
        }

        set
        {
            lastEmotions = value;
        }
    }

    public Dictionary<string, Perception> Perceptions
    {
        get
        {
            return perceptions;
        }

        set
        {
            perceptions = value;
        }
    }
    
}
