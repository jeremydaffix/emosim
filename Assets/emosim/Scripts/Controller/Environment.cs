using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// this class contains the environment of the simulation
// persons, interactive objects instances, interactive objects models, graphical resources,...
public class Environment : MonoBehaviour
{
    static Environment instance = null; // singleton


    private Dictionary<string, InteractiveObject> interactiveObjects = new Dictionary<string, InteractiveObject>(); // all models of interactive objects

    public GameObject personPrefab, interactiveObjectPrefab; // prefabs

    public int borderX = 9; // width = borderX * 2
    public int borderY = 4; // height = borderY * 2

    List<Person> persons = new List<Person>(); // all persons
    List<InteractiveObjectInstance> interactiveObjectInstances = new List<InteractiveObjectInstance>(); // all interactive object instances


    // sprites
    Sprite[] fruitsAtlas, foodAtlas;
    Sprite tree, skull, snake, amanita;

    

    public void Start()
    {
        instance = this;


        LoadResources();
        LoadInteractiveObjectModels();

        //CreateEnvironment();
    }


    void LoadResources()
    {
        // resources

        fruitsAtlas = Resources.LoadAll<Sprite>("Sprites/fruits");
        foodAtlas = Resources.LoadAll<Sprite>("Sprites/food");

        tree = Resources.Load<Sprite>("Sprites/tree");
        skull = Resources.Load<Sprite>("Sprites/skull");
        snake = Resources.Load<Sprite>("Sprites/snake");
        amanita = Resources.Load<Sprite>("Sprites/amanita");
    }

    public void LoadInteractiveObjectModels()
    {

        // model of interactive objects

        InteractiveObjects["apple"] = new InteractiveObject("apple", InteractiveObject.TYPE_FOOD, fruitsAtlas[0]);
        InteractiveObjects["apple"].NeedsSatisfied.Add("satiety", 1f);
        InteractiveObjects["apple"].NeedsSatisfied.Add("health", 0.2f);

        InteractiveObjects["apple"].TriggerBySight.Add("looksGood");
        InteractiveObjects["apple"].TriggerBySmell.Add("smellsGood");
        InteractiveObjects["apple"].TriggerByTaste.Add("tastesGood");

        InteractiveObjects["apple"].TriggerByTaste.Add("pleasureHormones");
        InteractiveObjects["apple"].TriggerByTaste.Add("smilingFace");
        InteractiveObjects["apple"].TriggerByTaste.Add("happyPosture");


        InteractiveObjects["carrot"] = new InteractiveObject("carrot", InteractiveObject.TYPE_FOOD, foodAtlas[3]);
        InteractiveObjects["carrot"].NeedsSatisfied.Add("satiety", 1f);
        InteractiveObjects["carrot"].NeedsSatisfied.Add("health", 0.5f);

        InteractiveObjects["carrot"].TriggerBySight.Add("looksBad");
        InteractiveObjects["carrot"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["carrot"].TriggerByTaste.Add("tastesNormal");


        InteractiveObjects["endive"] = new InteractiveObject("endive", InteractiveObject.TYPE_FOOD, fruitsAtlas[8]);
        InteractiveObjects["endive"].NeedsSatisfied.Add("satiety", 2f);
        InteractiveObjects["endive"].NeedsSatisfied.Add("health", 1f);

        InteractiveObjects["endive"].TriggerBySight.Add("looksNormal");
        InteractiveObjects["endive"].TriggerBySmell.Add("smellsBad");
        InteractiveObjects["endive"].TriggerByTaste.Add("tastesBad");

        InteractiveObjects["endive"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["endive"].TriggerByTaste.Add("closedPosture");


        InteractiveObjects["burger"] = new InteractiveObject("burger", InteractiveObject.TYPE_FOOD, foodAtlas[0]);
        InteractiveObjects["burger"].NeedsSatisfied.Add("satiety", 2f);
        InteractiveObjects["burger"].NeedsSatisfied.Add("health", -1f);

        InteractiveObjects["burger"].TriggerBySight.Add("looksGood");
        InteractiveObjects["burger"].TriggerBySmell.Add("smellsGood");
        InteractiveObjects["burger"].TriggerByTaste.Add("tastesGood");

        InteractiveObjects["burger"].TriggerByTaste.Add("pleasureHormones");
        InteractiveObjects["burger"].TriggerByTaste.Add("smilingFace");
        InteractiveObjects["burger"].TriggerByTaste.Add("happyPosture");


        /*InteractiveObjects["chanterelle"] = new InteractiveObject("chanterelle", InteractiveObject.TYPE_FOOD);
        InteractiveObjects["chanterelle"].NeedsSatisfied.Add("satiety", 1f);
        InteractiveObjects["chanterelle"].NeedsSatisfied.Add("health", 0f);

        InteractiveObjects["chanterelle"].TriggerBySight.Add("looksNormal");
        InteractiveObjects["chanterelle"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["chanterelle"].TriggerByTaste.Add("tastesGood");*/


        InteractiveObjects["amanita"] = new InteractiveObject("amanita", InteractiveObject.TYPE_FOOD, amanita);
        InteractiveObjects["amanita"].NeedsSatisfied.Add("satiety", 1f);
        InteractiveObjects["amanita"].NeedsSatisfied.Add("health", -8f);

        InteractiveObjects["amanita"].TriggerBySight.Add("looksGood");
        InteractiveObjects["amanita"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["amanita"].TriggerByTaste.Add("tastesNormal");

        InteractiveObjects["amanita"].TriggerByTaste.Add("wantToVomit");
        InteractiveObjects["amanita"].TriggerByTaste.Add("painHormones");
        InteractiveObjects["amanita"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["amanita"].TriggerByTaste.Add("closedPosture");




        InteractiveObjects["snake"] = new InteractiveObject("snake", InteractiveObject.TYPE_ANIMAL, snake);
        InteractiveObjects["snake"].NeedsSatisfied.Add("satiety", 1f);
        InteractiveObjects["snake"].NeedsSatisfied.Add("health", -8f); // a skake bites :(

        // a snake is SCARY
        InteractiveObjects["snake"].TriggerBySight.Add("fastHeartBeat");
        InteractiveObjects["snake"].TriggerBySight.Add("stomachAche");
        InteractiveObjects["snake"].TriggerBySight.Add("stressHormones");
        InteractiveObjects["snake"].TriggerBySight.Add("frightenedFace");
        InteractiveObjects["snake"].TriggerBySight.Add("frightenedPosture");
        InteractiveObjects["snake"].TriggerBySight.Add("looksTerrifying");

        InteractiveObjects["snake"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["snake"].TriggerByTaste.Add("tastesNormal");

        // if you get bitten, you will be sick
        InteractiveObjects["snake"].TriggerByTaste.Add("wantToVomit");
        InteractiveObjects["snake"].TriggerByTaste.Add("painHormones");
        InteractiveObjects["snake"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["snake"].TriggerByTaste.Add("closedPosture");



        InteractiveObjects["tree"] = new InteractiveObject("tree", InteractiveObject.TYPE_OBSTACLE, tree);

    }



    // create the environment, using configuration from Simulation class
    public void CreateEnvironment()
    {
        if (Simulation.Instance.Seed != -1) Random.InitState(Simulation.Instance.Seed);

        // persons

        for (int i = 0 ; i < Simulation.Instance.NbrPersons ; ++i)
        {
            CreateRandomPerson();
        }

        //Simulation.Instance.DesirabilityView = persons[0]; ////


        // interactive objects

        int mod = 5;

        for (int i = 0 ; i < Simulation.Instance.NbrObjects ; ++i)
        {

            if (i % mod == 0) CreateRandomInteractiveObject("apple");
            if (i % mod == 1) CreateRandomInteractiveObject("carrot");
            if (i % mod == 2) CreateRandomInteractiveObject("endive");
            if (i % mod == 3) CreateRandomInteractiveObject("burger");
            if (i % mod == 4) CreateRandomInteractiveObject("amanita");
        }


        // animals

        int mod2 = 1;

        for (int i = 0; i < Simulation.Instance.NbrAnimals; ++i)
        {

            if (i % mod2 == 0) CreateRandomInteractiveObject("snake");
        }


        // obstacles

        for (int i = 0; i < Simulation.Instance.NbrObstacles; ++i)
        {
            CreateRandomInteractiveObject("tree");
        }



    }


    // destroy the environment, persons, objects,... (so we can launch an other simulation !)
    public void DestroyEnvironment()
    {
        foreach(Person p in persons)
        {
            Destroy(p.gameObject);
        }

        foreach (InteractiveObjectInstance ioi in InteractiveObjectInstances)
        {
            Destroy(ioi.gameObject);
        }

        persons = new List<Person>();
        interactiveObjectInstances = new List<InteractiveObjectInstance>();
    }



    // create a person at a position
    public void CreatePerson(Vector3 pos = new Vector3())
    {
        GameObject obj = Instantiate(personPrefab, new Vector3(), new Quaternion());
        Person p = obj.GetComponent<Person>();

        persons.Add(p);

    }

    // create an interactive object (giving its name / identifier), at a position
    public void CreateInteractiveObject(string name, Vector3 pos = new Vector3())
    {
        GameObject obj = Instantiate(interactiveObjectPrefab, pos, new Quaternion());
        InteractiveObjectInstance i = obj.GetComponent<InteractiveObjectInstance>();

        i.InteractiveObjectName = name;
        i.InteractiveObject = InteractiveObjects[name];

        i.GetComponent<SpriteRenderer>().sprite = i.InteractiveObject.Sprite;

        if (i.InteractiveObject.Type == InteractiveObject.TYPE_OBSTACLE || i.InteractiveObject.Type == InteractiveObject.TYPE_ANIMAL)
        {
            i.GetComponent<BoxCollider2D>().enabled = true;
            i.GetComponent<BoxCollider2D>().isTrigger = false;

            if (i.InteractiveObject.Type == InteractiveObject.TYPE_ANIMAL)
            {
                i.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }

        interactiveObjectInstances.Add(i);
    }


    // create an interactive object at a random position
    public void CreateRandomInteractiveObject(string name)
    {
        CreateInteractiveObject(name, RandomCoords());
    }

    // create a person at a random position
    public void CreateRandomPerson()
    {
        CreatePerson(RandomCoords());
    }


    // move an interactive object an other (random) position
    public void RecycleInteractiveObject(InteractiveObjectInstance ioi)
    {
        ioi.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        ioi.transform.position = RandomCoords();
    }


    // generate random coordinates
    Vector3 RandomCoords()
    {
        int x, y;

        do
        {
            x = Random.Range(-borderX, borderX);
            y = Random.Range(-borderY, borderY);

        } while (!IsPlaceFree(new Vector3(x, y, 0))); // is the place free ?
         

        return new Vector3(x, y, 0);
    }


    // check if an object or a person isn't already here
    bool IsPlaceFree(Vector3 pos)
    {
        bool ret = true;

        foreach(InteractiveObjectInstance ioi in InteractiveObjectInstances)
        {
            if(Vector3.Distance(pos, ioi.transform.position) < 1f)
            {
                ret = false;
                break;
            }
        }

        foreach (Person p in Persons)
        {
            if (Vector3.Distance(pos, p.transform.position) < 1f)
            {
                ret = false;
                break;
            }
        }

        return ret;
    }



    // map with "standard" colors
    public void DisplayStandardMap()
    {
        foreach(InteractiveObjectInstance inst in interactiveObjectInstances)
        {
            inst.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        }
    }


    // map where the near objects are colored to fit the desirability for a specified person
    // for that, we use the scores stored in the "possible actions dictionary" created by the mind (emotional + cognitive)
    public void DisplayDesirabilityMap(Dictionary<GameObject, KeyValuePair<PersonAction, int>> mergedPossibleActions, int bestScore)
    {
        Person person = Simulation.Instance.DesirabilityView;


        foreach (KeyValuePair<GameObject, KeyValuePair<PersonAction, int>> pa in mergedPossibleActions)
        {
            Color color = new Color(1f, 1f, 1f);

            SpriteRenderer sr = pa.Key.GetComponent<SpriteRenderer>();
            float score = pa.Value.Value;
            PersonAction action = pa.Value.Key;

           

            if(action == person.PersonActions.ActionWalkToTarget || action == person.PersonActions.ActionEat)
            {
                if(score < 0f) // bad score
                {
                    if(score > -(bestScore / 2f)) color = new Color(0.2f, 0.2f, 0.5f); // pretty bad
                    else color = new Color(0.2f, 0.2f, 1.0f); // superbad
                }

                else // good score
                {
                    if (score < (bestScore / 2f)) color = new Color(0.5f, 0.2f, 0.2f); // pretty good
                    else color = new Color(1.0f, 0.2f, 0.2f); // super good
                }
            }

            else if(action == person.PersonActions.ActionFleeTarget) // FEAR
            {
                color = new Color(0f, 0f, 1f);
            }

            sr.color = color;
        }
    }





    public static Environment Instance
    {
        get
        {
            return instance;
        }

    }

    public Dictionary<string, InteractiveObject> InteractiveObjects
    {
        get
        {
            return interactiveObjects;
        }

        set
        {
            interactiveObjects = value;
        }
    }

    public List<Person> Persons
    {
        get
        {
            return persons;
        }

        set
        {
            persons = value;
        }
    }

    public List<InteractiveObjectInstance> InteractiveObjectInstances
    {
        get
        {
            return interactiveObjectInstances;
        }

        set
        {
            interactiveObjectInstances = value;
        }
    }
}
