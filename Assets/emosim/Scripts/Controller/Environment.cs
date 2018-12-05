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
    }



    public void CreateEnvironment()
    {
        
        // interactive objects

        CreateRandomInteractiveObject("apple");
        CreateRandomInteractiveObject("carrot");
        CreateRandomInteractiveObject("endive");

        CreateRandomInteractiveObject("apple");
        CreateRandomInteractiveObject("carrot");
        CreateRandomInteractiveObject("endive");

        CreateRandomInteractiveObject("apple");
        CreateRandomInteractiveObject("carrot");
        CreateRandomInteractiveObject("endive");

        CreateRandomInteractiveObject("apple");
        CreateRandomInteractiveObject("carrot");
        CreateRandomInteractiveObject("endive");


        // persons

        CreatePerson();
        CreatePerson();
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

        interactiveObjectInstances.Add(i);
    }


    public void CreateRandomInteractiveObject(string name)
    {
        int x = Random.Range(-borderX, borderX);
        int y = Random.Range(-borderY, borderY);

        CreateInteractiveObject(name, new Vector3(x, y, 0));
    }


    public void RecycleInteractiveObject(InteractiveObjectInstance ioi)
    {
        int x = Random.Range(-borderX, borderX);
        int y = Random.Range(-borderY, borderY);

        ioi.transform.position = new Vector3(x, y, 0);
    }



    public static Environment Instance
    {
        get
        {
            /*if (instance == null)
            {

                instance = new Environment();
            }*/

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
