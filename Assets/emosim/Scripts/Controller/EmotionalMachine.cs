using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EmotionalMachine
{

    Person person;

    List<SomaticMarker> somaticMemory = new List<SomaticMarker>();

    Dictionary<string, Perception> perceptions = new Dictionary<string, Perception>();
    
    Dictionary<string, Emotion> emotions = new Dictionary<string, Emotion>();

    Dictionary<string, Need> needs = new Dictionary<string, Need>();



    public EmotionalMachine(Person p)
    {
        person = p;


        // création des différents éléments (individus, émotions,...)
        // + tard : chargement dans des fichiers de config !


        // perceptions

        perceptions["normalHeartBeat"] = new Perception(person.Heart, 0);
        perceptions["fastHeartBeat"] = new Perception(person.Heart, 1);
        perceptions["slowHeartBeat"] = new Perception(person.Heart, 2);

        perceptions["stomachNormal"] = new Perception(person.Stomach, 0);
        perceptions["stomachAche"] = new Perception(person.Stomach, 1);
        perceptions["wantToVomit"] = new Perception(person.Stomach, 2);

        perceptions["noHormones"] = new Perception(person.Brain, 0);
        perceptions["painHormones"] = new Perception(person.Brain, 1);
        perceptions["pleasureHormones"] = new Perception(person.Brain, 2);
        perceptions["stressHormones"] = new Perception(person.Brain, 3);
        perceptions["relaxationHormones"] = new Perception(person.Brain, 4);

        perceptions["pokerFace"] = new Perception(person.Face, 0);
        perceptions["smilingFace"] = new Perception(person.Face, 1);
        perceptions["sadFace"] = new Perception(person.Face, 2);
        perceptions["frightenedFace"] = new Perception(person.Face, 3);

        perceptions["relaxedPosture"] = new Perception(person.Posture, 0);
        perceptions["closedFists"] = new Perception(person.Posture, 1);
        perceptions["closedPosture"] = new Perception(person.Posture, 2);
        perceptions["frightenedPosture"] = new Perception(person.Posture, 3);
        perceptions["happyPosture"] = new Perception(person.Posture, 4);


        perceptions["normalEyes"] = new Perception(person.Eyes, 0);
        perceptions["inTears"] = new Perception(person.Eyes, 1);


        perceptions["looksNormal"] = new Perception(person.EyesSensor, 0);
        perceptions["looksGood"] = new Perception(person.EyesSensor, 1);
        perceptions["looksBad"] = new Perception(person.EyesSensor, 2);
        perceptions["looksTerrifying"] = new Perception(person.EyesSensor, 3);

        perceptions["smellsNormal"] = new Perception(person.NoseSensor, 0);
        perceptions["smellsGood"] = new Perception(person.NoseSensor, 1);
        perceptions["smellsBad"] = new Perception(person.NoseSensor, 2);

        perceptions["tastesNormal"] = new Perception(person.PalateSensor, 0);
        perceptions["tastesGood"] = new Perception(person.PalateSensor, 1);
        perceptions["tastesBad"] = new Perception(person.PalateSensor, 2);



        // emotions

        emotions["fear"] = new Emotion("fear", -2);
        emotions["fear"].AddPerception(perceptions["fastHeartBeat"]).AddPerception(perceptions["stomachAche"]).AddPerception(perceptions["normalEyes"]).AddPerception(perceptions["stressHormones"]).AddPerception(perceptions["frightenedFace"]).AddPerception(perceptions["frightenedPosture"]).AddPerception(perceptions["looksTerrifying"]);

        emotions["pleasure"] = new Emotion("pleasure", 2);
        emotions["pleasure"].AddPerception(perceptions["fastHeartBeat"]).AddPerception(perceptions["stomachNormal"]).AddPerception(perceptions["normalEyes"]).AddPerception(perceptions["pleasureHormones"]).AddPerception(perceptions["smilingFace"]).AddPerception(perceptions["happyPosture"]).AddPerception(perceptions["looksGood"]).AddPerception(perceptions["tastesGood"]).AddPerception(perceptions["smellsGood"]);

        emotions["pain"] = new Emotion("pain", -1);
        emotions["pain"].AddPerception(perceptions["fastHeartBeat"]).AddPerception(perceptions["stomachNormal"]).AddPerception(perceptions["inTears"]).AddPerception(perceptions["painHormones"]).AddPerception(perceptions["sadFace"]).AddPerception(perceptions["closedFists"]);

        emotions["sadness"] = new Emotion("sadness", -1);
        emotions["sadness"].AddPerception(perceptions["slowHeartBeat"]).AddPerception(perceptions["stomachAche"]).AddPerception(perceptions["inTears"]).AddPerception(perceptions["painHormones"]).AddPerception(perceptions["sadFace"]).AddPerception(perceptions["closedPosture"]).AddPerception(perceptions["smellsBad"]).AddPerception(perceptions["looksBad"]).AddPerception(perceptions["tastesBad"]);

        emotions["stress"] = new Emotion("stress", -1);
        emotions["stress"].AddPerception(perceptions["fastHeartBeat"]).AddPerception(perceptions["stomachAche"]).AddPerception(perceptions["normalEyes"]).AddPerception(perceptions["stressHormones"]).AddPerception(perceptions["sadFace"]).AddPerception(perceptions["closedPosture"]);

        emotions["relaxation"] = new Emotion("relaxation", 1);
        emotions["relaxation"].AddPerception(perceptions["slowHeartBeat"]).AddPerception(perceptions["stomachNormal"]).AddPerception(perceptions["normalEyes"]).AddPerception(perceptions["relaxationHormones"]).AddPerception(perceptions["smilingFace"]).AddPerception(perceptions["relaxedPosture"]);

        emotions["sickness"] = new Emotion("sickness", -2);
        emotions["sickness"].AddPerception(perceptions["normalHeartBeat"]).AddPerception(perceptions["wantToVomit"]).AddPerception(perceptions["normalEyes"]).AddPerception(perceptions["painHormones"]).AddPerception(perceptions["sadFace"]).AddPerception(perceptions["closedPosture"]);

        emotions["neutral"] = new Emotion("neutral", 1);
        emotions["neutral"].AddPerception(perceptions["normalHeartBeat"]).AddPerception(perceptions["stomachNormal"]).AddPerception(perceptions["normalEyes"]).AddPerception(perceptions["noHormones"]).AddPerception(perceptions["pokerFace"]).AddPerception(perceptions["relaxedPosture"]);



        // besoins

        needs["health"] = new Need("health", 10);
        needs["satiety"] = new Need("satiety", 5);



        // marqueurs somatiques innés

        SomaticMarker amanitaSM = new SomaticMarker(new MentalImage(MentalImage.TYPE_OBJECT, Environment.Instance.InteractiveObjects["amanita"]));
        amanitaSM.Perceptions.Add(perceptions["wantToVomit"]);
        amanitaSM.Perceptions.Add(perceptions["painHormones"]);
        SomaticMemory.Add(amanitaSM);



        // connaissances (= on sait que, mais sans trigger de perceptions)
        // (dans mind ???)

    }


    public int CalcMood()
    {
        Dictionary<Emotion, int> em = CalcEmotions();

        int score = 0;

        foreach(KeyValuePair<Emotion, int> kvp in em)
        {
            Debug.Log(kvp.Key.Name + " : " + kvp.Value);
            score += kvp.Key.DesirabilityScore;

            // ...
        }

        Debug.Log("MOOD : " + score);

        return score;
    }

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

        em.OrderByDescending(i => i.Key);

        return em;
    }


    public void SeeObject(InteractiveObject obj)
    {
        foreach(string s in obj.TriggerBySight)
        {
            if(perceptions.ContainsKey(s))
            {
                Perception p = perceptions[s];

                p.Organ.State = p.State;
            }
        }
    }


    public void SmellObject(InteractiveObject obj)
    {
        foreach (string s in obj.TriggerBySmell)
        {
            if (perceptions.ContainsKey(s))
            {
                Perception p = perceptions[s];

                p.Organ.State = p.State;
            }
        }
    }


    public void EatObject(InteractiveObject obj)
    {
        foreach (string s in obj.TriggerByTaste)
        {
            if (perceptions.ContainsKey(s))
            {
                Perception p = perceptions[s];

                p.Organ.State = p.State;
            }
        }

        foreach (KeyValuePair<string, int> kvp in obj.NeedsSatisfied)
        {
            if (needs.ContainsKey(kvp.Key))
            {
                Need n = needs[kvp.Key];

                n.CurrentScore += kvp.Value;
            }
        }
    }


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

            // needs ??
        }
    }


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
}
