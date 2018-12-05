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

    // for physical perceptions from other people / objects (looks nice / ugly, smells good / bad, etc)
    Organ eyesSensor;
    Organ noseSensor;
    Organ palateSensor;

    EmotionalMachine emotionalMachine;


    public float LookRange = 4f;


    void Start()
    {
        EmotionalMachine = new EmotionalMachine();
        Mind = new Mind(this);

        heart = new Organ("heart");
        stomach = new Organ("stomach");
        brain = new Organ("brain");
        face = new Organ("face");
        posture = new Organ("posture");
        eyes = new Organ("eyes");

        eyesSensor = new Organ("eyesSensor");
        noseSensor = new Organ("noseSensor");
        palateSensor = new Organ("palateSensor");


        emotionalMachine.Create(this);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Simulation.Instance.TurnCpt % Simulation.Instance.TurnDuration == 0) // 1 turn
        {
            //Debug.Log("PERSON TURN");

            mind.TakeDecision();
        }
    }


    // return all objects and persos we can see
    // TODO detect with more senses (smell,...)
    public List<GameObject> LookForThings()
    {
        List<GameObject> l = new List<GameObject>();

        foreach (Person p in Environment.Instance.Persons)
        {
            float dist = Vector3.Distance(p.transform.position, transform.position);

            if (dist <= LookRange) l.Add(p.gameObject);
        }


        foreach (InteractiveObjectInstance i in Environment.Instance.InteractiveObjectInstances)
        {
            float dist = Vector3.Distance(i.transform.position, transform.position);

            if (dist <= LookRange) l.Add(i.gameObject);
        }


        return l;
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

}
