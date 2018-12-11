using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// this class contains a person / agent
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


    // for collisions / obstacles avoidance
    /*GameObject collidingWith = null;
    int collidingSince = 0;*/
    int isBlockedSince = 0;
    Vector3 lastPos;
    PersonAction lastAction;


    public float LookRange = 3.0f; // how far we can see things


    int deadSince = -1; // am I dead ?

    public Sprite skull;



    void Start()
    {
        // instantiate everything

        EmotionalMachine = new EmotionalMachine();
        CognitiveMachine = new CognitiveMachine();

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


        cognitiveMachine.Create(this);
        emotionalMachine.Create(this);

        personActions = new PersonActions(this);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Simulation.Instance.Playing && DeadSince == -1 && Simulation.Instance.FrameCpt % Simulation.Instance.TurnDuration == 0) // 1 turn
        {
            int currentTurn = Simulation.Instance.FrameCpt / Simulation.Instance.TurnDuration;

            Vector3 initPos = transform.position;

            KeyValuePair<PersonAction, GameObject> actionToDo = mind.TakeDecision(); // calc the best action
            actionToDo.Key(actionToDo.Value); // do the action, giving an optional target

            ApplyNeeds(); // each turn the satisfaction of needs (satiety, health,...) decreases, so we have to fulfill them again

            CheckIfBlocked(lastPos, lastAction);
            lastPos = initPos;
            lastAction = actionToDo.Key;

            //Debug.Log(lastAction.Method.Name);
        }
    }


    void CheckIfBlocked(Vector3 initPos, PersonAction actionToDo)
    {
        // detects if the agent is blocked in his will to walk towards an object
        // we have to check one frame later (that's why we store lastPost and lastAction), because rigidbody.moveposition() doesn't move the object instantly !

        if (actionToDo == PersonActions.ActionWalkToTarget)
        {
            // physics collision with obstacles is not detected every time
            // so we check if the agent has moved during the action !

            if ((IsBlockedSince == 0) && ((transform.position - initPos).magnitude < 0.001f))
            {
                IsBlockedSince = Simulation.Instance.FrameCpt;
            }
        }

        else
        {
            IsBlockedSince = 0;
        }
    }


    void ApplyNeeds()
    {
        // needs

        foreach (KeyValuePair<string, Need> kvp in CognitiveMachine.Needs)
        {
            kvp.Value.CurrentScore -= kvp.Value.DecreaseByTurn;

            if (kvp.Value.CurrentScore <= 0f)
            {
                if(kvp.Key.Equals("health")) Die(kvp.Value); // health = 0 : RIP
                else CognitiveMachine.Needs["health"].CurrentScore -= 0.5f; // satiety or other = 0 : slowly dying
            }

        }
    }


    // RIP IN PEACE LITTLE ANGEL
    void Die(Need from)
    {
        //Destroy(gameObject, 3f);
        GetComponent<SpriteRenderer>().sprite = skull;
        GetComponentInChildren<TextMesh>().text = "";
        GetComponent<LineRenderer>().enabled = false;

        DeadSince = Simulation.Instance.FrameCpt / Simulation.Instance.TurnDuration;

        //Debug.Log("DIE FROM " + from.Name);
    }




    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("COLLENTER");

        collidingWith = collision.gameObject;
        CollidingSince = Simulation.Instance.FrameCpt;
    }


    void OnCollisionExit2D(Collision2D collision)
    {

        //Debug.Log("COLLEXIT");

        CollidingWith = null;
        CollidingSince = 0;
    }*/


    // select person for display
    private void OnMouseDown()
    {
        //Debug.Log("CLICK PERSON");

        Environment.Instance.DisplayStandardMap();
        Simulation.Instance.DesirabilityView = this;
        UIController.Instance.DisplayedPerson = this;
        UIController.Instance.DisplayedObject = null;
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

    public int IsBlockedSince
    {
        get
        {
            return isBlockedSince;
        }

        set
        {
            isBlockedSince = value;
        }
    }

    public int DeadSince
    {
        get
        {
            return deadSince;
        }

        set
        {
            deadSince = value;
        }
    }


}
