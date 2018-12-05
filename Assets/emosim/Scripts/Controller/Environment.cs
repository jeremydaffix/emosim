using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Environment : MonoBehaviour
{
    static Environment instance = null; // singleton


    private Dictionary<string, InteractiveObject> interactiveObjects = new Dictionary<string, InteractiveObject>();

    public GameObject personPrefab, interactiveObjectPrefab;

    public int borderX = 9;
    public int borderY = 4;

    List<Person> persons = new List<Person>();
    List<InteractiveObjectInstance> interactiveObjectInstances = new List<InteractiveObjectInstance>();


    Sprite[] fruitsAtlas, foodAtlas;
    Sprite tree;

    

    public void Start()
    {
        instance = this;


        LoadResources();
        LoadInteractiveObjectModels();

        CreateEnvironment();


    }


    void LoadResources()
    {
        // resources

        fruitsAtlas = Resources.LoadAll<Sprite>("Sprites/fruits");
        foodAtlas = Resources.LoadAll<Sprite>("Sprites/food");

        tree = Resources.Load<Sprite>("Sprites/tree");
    }

    public void LoadInteractiveObjectModels()
    {

        // model of interactive objects

        InteractiveObjects["apple"] = new InteractiveObject("apple", InteractiveObject.TYPE_FOOD, fruitsAtlas[0]);
        InteractiveObjects["apple"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["apple"].NeedsSatisfied.Add("health", 1);

        InteractiveObjects["apple"].TriggerBySight.Add("looksGood");
        InteractiveObjects["apple"].TriggerBySmell.Add("smellsGood");
        InteractiveObjects["apple"].TriggerByTaste.Add("tastesGood");

        InteractiveObjects["apple"].TriggerByTaste.Add("pleasureHormones");
        InteractiveObjects["apple"].TriggerByTaste.Add("smilingFace");
        InteractiveObjects["apple"].TriggerByTaste.Add("happyPosture");


        InteractiveObjects["carrot"] = new InteractiveObject("carrot", InteractiveObject.TYPE_FOOD, foodAtlas[3]);
        InteractiveObjects["carrot"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["carrot"].NeedsSatisfied.Add("health", 2);

        InteractiveObjects["carrot"].TriggerBySight.Add("looksBad");
        InteractiveObjects["carrot"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["carrot"].TriggerByTaste.Add("tastesNormal");


        InteractiveObjects["endive"] = new InteractiveObject("endive", InteractiveObject.TYPE_FOOD, fruitsAtlas[8]);
        InteractiveObjects["endive"].NeedsSatisfied.Add("satiety", 2);
        InteractiveObjects["endive"].NeedsSatisfied.Add("health", 3);

        InteractiveObjects["endive"].TriggerBySight.Add("looksNormal");
        InteractiveObjects["endive"].TriggerBySmell.Add("smellsBad");
        InteractiveObjects["endive"].TriggerByTaste.Add("tastesBad");

        InteractiveObjects["endive"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["endive"].TriggerByTaste.Add("closedPosture");


        InteractiveObjects["chanterelle"] = new InteractiveObject("chanterelle", InteractiveObject.TYPE_MUSHROOM);
        InteractiveObjects["chanterelle"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["chanterelle"].NeedsSatisfied.Add("health", 0);

        InteractiveObjects["chanterelle"].TriggerBySight.Add("looksNormal");
        InteractiveObjects["chanterelle"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["chanterelle"].TriggerByTaste.Add("tastesGood");


        InteractiveObjects["amanita"] = new InteractiveObject("amanita", InteractiveObject.TYPE_MUSHROOM);
        InteractiveObjects["amanita"].NeedsSatisfied.Add("satiety", 1);
        InteractiveObjects["amanita"].NeedsSatisfied.Add("health", -4);

        InteractiveObjects["amanita"].TriggerBySight.Add("looksGood");
        InteractiveObjects["amanita"].TriggerBySmell.Add("smellsNormal");
        InteractiveObjects["amanita"].TriggerByTaste.Add("tastesNormal");

        InteractiveObjects["amanita"].TriggerByTaste.Add("wantToVomit");
        InteractiveObjects["amanita"].TriggerByTaste.Add("painHormones");
        InteractiveObjects["amanita"].TriggerByTaste.Add("sadFace");
        InteractiveObjects["amanita"].TriggerByTaste.Add("closedPosture");



        InteractiveObjects["tree"] = new InteractiveObject("tree", InteractiveObject.TYPE_OBSTACLE, tree);

    }



    public void CreateEnvironment()
    {

        // persons

        for (int i = 0 ; i < Simulation.Instance.NbrPersons ; ++i)
        {
            CreatePerson();
        }


        // interactive objects

        for (int i = 0 ; i < Simulation.Instance.NbrObjects ; ++i)
        {
            if (i % 3 == 0) CreateRandomInteractiveObject("apple");
            if (i % 3 == 1) CreateRandomInteractiveObject("carrot");
            if (i % 3 == 2) CreateRandomInteractiveObject("endive");
        }


        for (int i = 0; i < Simulation.Instance.NbrObstacles; ++i)
        {
            CreateRandomInteractiveObject("tree");
        }



    }




    public void CreatePerson(Vector3 pos = new Vector3())
    {
        GameObject obj = Instantiate(personPrefab, new Vector3(), new Quaternion());
        Person p = obj.GetComponent<Person>();

        persons.Add(p);

    }

    public void CreateInteractiveObject(string name, Vector3 pos = new Vector3())
    {
        GameObject obj = Instantiate(interactiveObjectPrefab, pos, new Quaternion());
        InteractiveObjectInstance i = obj.GetComponent<InteractiveObjectInstance>();

        i.InteractiveObjectName = name;
        i.InteractiveObject = InteractiveObjects[name];

        i.GetComponent<SpriteRenderer>().sprite = i.InteractiveObject.Sprite;

        if (i.InteractiveObject.Type == InteractiveObject.TYPE_OBSTACLE) i.GetComponent<BoxCollider2D>().enabled = true;

        interactiveObjectInstances.Add(i);
    }


    public void CreateRandomInteractiveObject(string name)
    {
        CreateInteractiveObject(name, RandomCoords());
    }


    public void RecycleInteractiveObject(InteractiveObjectInstance ioi)
    {
        ioi.transform.position = RandomCoords();
    }


    Vector3 RandomCoords()
    {
        int x, y;

        do
        {
            x = Random.Range(-borderX, borderX);
            y = Random.Range(-borderY, borderY);

        } while (!IsPlaceFree(new Vector3(x, y, 0)));
         

        return new Vector3(x, y, 0);
    }

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

        return ret;
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
