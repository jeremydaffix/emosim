using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : MonoBehaviour
{
    // + tard : faire une factory !!!

    Mind mind;

    // for perceptions from own body
    Organ heart;
    Organ stomach;
    Organ brain;
    Organ face;
    Organ posture;
    Organ eyes;

    // for perceptions from other people / objects
    Organ eyesSensor;
    Organ noseSensor;
    Organ palateSensor;

    EmotionalMachine emotionalMachine;


    float detectableFrom = 5.0f;




    void Start()
    {
        EmotionalMachine = new EmotionalMachine(this);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Simulation.Instance.TurnCpt % Simulation.Instance.TurnDuration == 0) // 1 turn
        {
            Debug.Log("PERSON TURN");
        }
    }



    // TODO detect with more senses (sight, smell,...)
    public List<InteractiveObject> CheckForInteractiveObjects(float dist)
    {
        List<InteractiveObject> objList = new List<InteractiveObject>();



        return objList;
    }









    public Mind Mind
    {
        get
        {
            return mind;
        }

        set
        {
            mind = value;
        }
    }

    public Organ Heart
    {
        get
        {
            return heart;
        }

        set
        {
            heart = value;
        }
    }

    public Organ Stomach
    {
        get
        {
            return stomach;
        }

        set
        {
            stomach = value;
        }
    }

    public Organ Brain
    {
        get
        {
            return brain;
        }

        set
        {
            brain = value;
        }
    }

    public Organ Face
    {
        get
        {
            return face;
        }

        set
        {
            face = value;
        }
    }

    public Organ Posture
    {
        get
        {
            return posture;
        }

        set
        {
            posture = value;
        }
    }

    public Organ Eyes
    {
        get
        {
            return eyes;
        }

        set
        {
            eyes = value;
        }
    }

    public Organ EyesSensor
    {
        get
        {
            return eyesSensor;
        }

        set
        {
            eyesSensor = value;
        }
    }

    public Organ NoseSensor
    {
        get
        {
            return noseSensor;
        }

        set
        {
            noseSensor = value;
        }
    }

    public Organ PalateSensor
    {
        get
        {
            return palateSensor;
        }

        set
        {
            palateSensor = value;
        }
    }

    public EmotionalMachine EmotionalMachine
    {
        get
        {
            return emotionalMachine;
        }

        set
        {
            emotionalMachine = value;
        }
    }

    public float DetectableFrom
    {
        get
        {
            return detectableFrom;
        }

        set
        {
            detectableFrom = value;
        }
    }
}
