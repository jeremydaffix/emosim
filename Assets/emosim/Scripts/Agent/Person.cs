using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : MonoBehaviour
{
    Mind mind; // decisions center

    EmotionalMachine emotionalMachine; // emotional part of decisions
    CognitiveMachine cognitiveMachine; // cognitive part of decisions



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


    PersonActions personActions; // methods to make the agent do something (walk,...)

    GameObject collidingWith = null;


    public float LookRange = 3.0f; // how far we can see things


    void Start()
    {
        EmotionalMachine = new EmotionalMachine();
        CognitiveMachine = new CognitiveMachine(this);

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

        personActions = new PersonActions(this);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Simulation.Instance.TurnCpt % Simulation.Instance.TurnDuration == 0) // 1 turn
        {
            mind.TakeDecision();
        }
    }










    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("COLLENTER");

        collidingWith = collision.gameObject;
    }


    void OnCollisionExit2D(Collision2D collision)
    {

        //Debug.Log("COLLEXIT");

        CollidingWith = null;
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

    public PersonActions PersonActions
    {
        get
        {
            return personActions;
        }

        set
        {
            personActions = value;
        }
    }

    public GameObject CollidingWith
    {
        get
        {
            return collidingWith;
        }

        set
        {
            collidingWith = value;
        }
    }

    public CognitiveMachine CognitiveMachine
    {
        get
        {
            return cognitiveMachine;
        }

        set
        {
            cognitiveMachine = value;
        }
    }
}
