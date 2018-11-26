using UnityEngine;
using System.Collections;

public class EmotionalMachine
{

    Person person;
    

    Perception  fastHeartBeat, slowHeartBeat, normalHeartBeat, // heart
                stomachNormal, wantToVomit, stomachAche, // stomach
                noHormones, painHormones, pleasureHormones, stressHormones, relaxationHormones,  // brain
                smilingFace, pokerFace, sadFace, frightenedFace, // face
                closedFists, relaxedPosture, closedPosture, frightenedPosture, happyPosture, // posture
                normalEyes, inTears, // eyes
                looksNormal, looksGood, looksBad, looksTerrifying, // eyesSensor
                smellsNormal, smellsGood, smellsBad, // noseSensor
                tastesNormal, tastesGood, tastesBad; // palateSensor


    //Emotion fear, pleasure, pain, anger, joy, sadness, pride, shame, stress, sickness;
    Emotion fear, pleasure, pain, sadness, stress, relaxation, sickness;




    public EmotionalMachine(Person p)
    {
        person = p;


        // création des différents éléments (individus, émotions,...)
        // + tard : chargement dans des fichiers de config !


        // perceptions

        normalHeartBeat = new Perception(person.Heart, 0);
        fastHeartBeat = new Perception(person.Heart, 1);
        slowHeartBeat = new Perception(person.Heart, 2);

        stomachNormal = new Perception(person.Stomach, 0);
        stomachAche = new Perception(person.Stomach, 1);
        wantToVomit = new Perception(person.Stomach, 2);

        noHormones = new Perception(person.Brain, 0);
        painHormones = new Perception(person.Brain, 1);
        pleasureHormones = new Perception(person.Brain, 2);
        stressHormones = new Perception(person.Brain, 3);
        relaxationHormones = new Perception(person.Brain, 4);

        pokerFace = new Perception(person.Face, 0);
        smilingFace = new Perception(person.Face, 1);
        sadFace = new Perception(person.Face, 2);
        frightenedFace = new Perception(person.Face, 3);

        relaxedPosture = new Perception(person.Posture, 0);
        closedFists = new Perception(person.Posture, 1);
        closedPosture = new Perception(person.Posture, 2);
        frightenedPosture = new Perception(person.Posture, 3);
        happyPosture = new Perception(person.Posture, 4);


        normalEyes = new Perception(person.Eyes, 0);
        inTears = new Perception(person.Eyes, 1);


        looksNormal = new Perception(person.EyesSensor, 0);
        looksGood = new Perception(person.EyesSensor, 1);
        looksBad = new Perception(person.EyesSensor, 2);
        looksTerrifying = new Perception(person.EyesSensor, 3);

        smellsNormal = new Perception(person.NoseSensor, 0);
        smellsGood = new Perception(person.NoseSensor, 1);
        smellsBad = new Perception(person.NoseSensor, 2);

        tastesNormal = new Perception(person.PalateSensor, 0);
        tastesGood = new Perception(person.PalateSensor, 1);
        tastesBad = new Perception(person.PalateSensor, 2);



        //fastHeartBeat = new Perception();


        // emotions


        //Emotion fear, pleasure, pain, anger, joy, sadness, pride, shame, stress, sickness;


        fear = new Emotion("fear", -2);
        fear.AddPerception(fastHeartBeat).AddPerception(stomachAche).AddPerception(normalEyes).AddPerception(stressHormones).AddPerception(frightenedFace).AddPerception(frightenedPosture).AddPerception(looksTerrifying);

        pleasure = new Emotion("pleasure", 2);
        pleasure.AddPerception(fastHeartBeat).AddPerception(stomachNormal).AddPerception(normalEyes).AddPerception(pleasureHormones).AddPerception(smilingFace).AddPerception(happyPosture).AddPerception(looksGood).AddPerception(tastesGood).AddPerception(smellsGood);

        pain = new Emotion("pain", -1);
        pain.AddPerception(fastHeartBeat).AddPerception(stomachNormal).AddPerception(inTears).AddPerception(painHormones).AddPerception(sadFace).AddPerception(closedFists);
        
        sadness = new Emotion("sadness", -1);
        sadness.AddPerception(slowHeartBeat).AddPerception(stomachAche).AddPerception(inTears).AddPerception(painHormones).AddPerception(sadFace).AddPerception(closedPosture).AddPerception(smellsBad).AddPerception(looksBad).AddPerception(tastesBad);

        stress = new Emotion("stress", -1);
        sadness.AddPerception(fastHeartBeat).AddPerception(stomachAche).AddPerception(normalEyes).AddPerception(stressHormones).AddPerception(sadFace).AddPerception(closedPosture);

        relaxation = new Emotion("relaxation", 1);
        sadness.AddPerception(slowHeartBeat).AddPerception(stomachNormal).AddPerception(normalEyes).AddPerception(relaxationHormones).AddPerception(smilingFace).AddPerception(relaxedPosture);

        sickness = new Emotion("sickness", -2);
        sickness.AddPerception(normalHeartBeat).AddPerception(wantToVomit).AddPerception(normalEyes).AddPerception(painHormones).AddPerception(sadFace).AddPerception(closedPosture);
        


        // besoins

        // images mentales innées

        // marqueurs somatiques innés
    }



}
